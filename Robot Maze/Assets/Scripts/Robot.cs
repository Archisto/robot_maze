using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class Robot : MonoBehaviour
    {

		bool canAct = true;

        private Instruction[] instructions;
        private int currentInstruction;
        private bool readyForNextInstruction;
        private bool active;
        private bool broken = false;

        private float startTime;
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Quaternion startRotation;
        private Quaternion targetRotation;

        private LayerMask layerMask;

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                if (active)
                {
                    //Activate();
                }
            }
        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start()
        {
            layerMask = LayerMask.GetMask("Robot", "Wall");
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
			/*if (Input.GetKeyDown (KeyCode.Space)) {
				MoveForward ();
			} else if (Input.GetKeyDown (KeyCode.LeftControl)) {
				RotateLeft ();
			} else if (Input.GetKeyDown (KeyCode.LeftShift)) {
				RotateRight ();
			}*/
			
            if (active)
            {
                if (readyForNextInstruction)
                {
                    readyForNextInstruction = false;

                    if (currentInstruction < instructions.Length - 1)
                    {
                        currentInstruction++;
                    }
                    else
                    {
                        Deactivate();
                    }
                }

                if (active)
                {
                    FollowCurrentInstruction();
                }
            }

            // Testing purposes only
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instruction[] debugInsts = FindObjectOfType<InstructionList>().Instructions;
                    Activate(debugInsts);
                }
            }
        }

		public void Activate(Instruction[] gotInstructions)
        {
			if (!active && !broken)
            {
                currentInstruction = 0;
				instructions = gotInstructions;
				active = true;
			}
        }

        public void Deactivate()
        {
            if (active)
            {
                active = false;
            }
        }

        private void FollowCurrentInstruction()
        {
            switch (instructions[currentInstruction].type)
            {
                case InstructionType.MoveForward:
                {
                    MoveForward();
                    break;
                }
                case InstructionType.TurnLeft:
                {
                    RotateLeft();
                    break;
                }
                case InstructionType.TurnRight:
                {
                    RotateRight();
                    break;
                }
                case InstructionType.UTurn:
                {
                    UTurn();
                    break;
                }
                case InstructionType.PushItem:
                {
                    DropItem();
                    break;
                }
                case InstructionType.PickUpItem:
                {
                    PickUpItem();
                    break;
                }
                case InstructionType.DropItem:
                {
                    DropItem();
                    break;
                }
            }
        }

        private void InitMovement()
        {
            startTime = Time.time;

            startPosition = transform.position;

            targetPosition = transform.position +
                transform.forward * GameManager.Instance.gridSideLength;

            canAct = false;
        }

        private void InitRotation(bool right, bool uTurn)
        {
            startTime = Time.time;

            startRotation = transform.rotation;

            float straightAngle = (right ? 90 : -90);
            float degrees = transform.rotation.eulerAngles.y + (uTurn ? 180 : straightAngle);
            targetRotation = Quaternion.Euler(0, degrees, 0);

            canAct = false;
        }

        private void FinishAction()
        {
            canAct = true;
            readyForNextInstruction = true;
        }

        public void MoveForward()
        {
            if (canAct)
            {
                if (!ObstacleAhead(1))
                {
                    InitMovement();
                    //StartCoroutine(MoveSquare());
                }
                else
                {
                    Deactivate();
                    Debug.Log(name + " cannot move because there's an obstacle in the way.");
                }
            }

            if (active)
            {
                Move();
            }
        }

        public void RotateLeft()
        {
            if (canAct)
            {
                InitRotation(false, false);
                //StartCoroutine(SpinLeft());
            }

            Rotate();
        }

        public void RotateRight()
        {
            if (canAct)
            {
                InitRotation(true, false);
                //StartCoroutine(SpinRight());
            }

            Rotate();
        }

        private void UTurn()
        {

        }

        private void PushItem()
        {

        }

        private void PickUpItem()
        {

        }

        private void DropItem()
        {

        }


		IEnumerator MoveSquare(){
			canAct = false;
			for(int i = 0; i < 60 / GameManager.Instance.robotActionDuration; i++){

                float gridSideLength = GameManager.Instance.gridSideLength;
                //float ratio = Time.time - startTime / GameManager.Instance.robotActionSpeed;

                if (Vector3.Distance(transform.position, targetPosition) <
                        0.5f * gridSideLength ||
                        !CrashedIntoAnotherRobot())
                {
                    transform.Translate(Vector3.forward * gridSideLength * 1 / 60);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                else
                {
                    broken = true;
					transform.Find ("Point light").gameObject.SetActive (false);
					transform.Find ("Point light (1)").gameObject.SetActive (false);
                    Deactivate();
                    Debug.Log(name + " crashed into another robot.");
                    yield break;
                }
			}
            FinishAction();
        }

        private void Move()
        {
            float ratio = (Time.time - startTime) / GameManager.Instance.robotActionDuration;

            if (ratio > 0.7f || !CrashedIntoAnotherRobot())
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, ratio);
            }
            else
            {
                broken = true;
                transform.Find("Point light").gameObject.SetActive(false);
                transform.Find("Point light (1)").gameObject.SetActive(false);
                Deactivate();
                Debug.Log(name + " crashed into another robot.");
            }

            if (ratio >= 1.0f)
            {
                transform.position = targetPosition;
				if (transform.position.x < -3f && transform.position.x > -4f && transform.position.z < 7f && transform.position.z > 6f) 
				{
					FindObjectOfType<Win> ().DoWin ();
				}
                FinishAction();
            }
        }

        private void Rotate()
        {
            float ratio = (Time.time - startTime) / GameManager.Instance.robotActionDuration;

            Debug.Log(name + " transform.rotation: " + transform.rotation.eulerAngles);
            Debug.Log(name + "startRotation: " + startRotation.eulerAngles);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, ratio);

            if (ratio >= 1.0f)
            {
                transform.rotation = targetRotation;
                FinishAction();
            }
        }

        IEnumerator SpinLeft(){
			canAct = false;
			for(int i = 0; i < 90; i++){
				transform.Rotate (Vector3.down, 1f);
				yield return new WaitForSeconds (Time.deltaTime);
			}
            FinishAction();
        }

		IEnumerator SpinRight(){
			canAct = false;
			for(int i = 0; i < 90; i++){
				transform.Rotate (Vector3.down, -1f);
				yield return new WaitForSeconds (Time.deltaTime);
			}
            FinishAction();
        }

        private Vector3 raycastStart = Vector3.zero;
        private Vector3 raycastEnd = Vector3.zero;
        private bool ObstacleAhead(Vector3 raycastOrigin, 
                                   float maxDistance,
                                   bool robotsOnly)
        {
            float y = 0.3f;

            raycastStart = raycastOrigin;
            raycastStart.y += y;
            raycastEnd = raycastStart + transform.forward * maxDistance;

            Ray ray = new Ray(raycastStart, transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
            {
                Robot hitRobot = hitInfo.transform.GetComponent<Robot>();
                if (!robotsOnly || hitRobot != null || hitRobot != this)
                {
                    Debug.Log("Raycast hit: " + hitInfo.transform.name);
                    return true;
                }
            }

            return false;
        }

		private void OverGoal()
		{
			raycastStart = transform.position;
			raycastEnd = raycastStart - transform.up * 20;
			Debug.Log (raycastStart + " is the start");
			Debug.Log (raycastEnd + " is what should be hit");

			Ray ray = new Ray (raycastStart, raycastEnd);
			RaycastHit hitInfo;
			if (Physics.Raycast (ray, out hitInfo, 20f)) 
			{
				Win hitWin = hitInfo.transform.GetComponent<Win> ();
				if (hitWin != null) 
				{
					
					hitWin.DoWin ();
				}
			}

		}

        private bool ObstacleAhead(float maxDistance)
        {
            return ObstacleAhead(transform.position, maxDistance, false);
        }

        private bool CrashedIntoAnotherRobot()
        {
            Vector3 offset = transform.right * 0.3f * GameManager.Instance.gridSideLength;
            Vector3 raycastOrigin1 = transform.position + -1f * offset;
            Vector3 raycastOrigin2 = transform.position + offset;

            float maxDistance = 0.5f * GameManager.Instance.gridSideLength;

            return ObstacleAhead(raycastOrigin1, maxDistance, true) ||
                ObstacleAhead(raycastOrigin2, maxDistance, true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(raycastStart, raycastEnd);
        }

		public bool IsBroken(){
			return broken;
		}
    }
}