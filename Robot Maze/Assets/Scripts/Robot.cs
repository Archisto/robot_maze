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

					FollowCurrentInstruction ();

                    if (currentInstruction < instructions.Length - 1)
                    {
                        currentInstruction++;
                    }
                    else
                    {
                        Deactivate();
                    }
                }
                else
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
				readyForNextInstruction = true;
				instructions = gotInstructions;
				active = true;

				startPosition = transform.position;
				startRotation = transform.rotation;
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

		public void MoveForward()
		{
			if (canAct) {
                if (!ObstacleAhead(1))
                {
                    StartCoroutine(MoveSquare());
                }
                else
                {
                    Deactivate();
                    Debug.Log(name + " cannot move because there's an obstacle in the way.");
                }
			}
		}

		public void RotateLeft(){
			if (canAct) {
				StartCoroutine (SpinLeft ());
			}
		}

		public void RotateRight(){
			if (canAct) {
				StartCoroutine (SpinRight ());
			}
		}

		IEnumerator MoveSquare(){
			canAct = false;
			for(int i = 0; i < 60 / GameManager.Instance.robotActionSpeed; i++){

                if (!ObstacleAhead(0.4f))
                {
                    transform.Translate(Vector3.forward * GameManager.Instance.gridSideLength * 1 / 60);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                else
                {
                    broken = true;
                    Deactivate();
                    Debug.Log(name + " crashed into something");
                    yield break;
                }
			}
			canAct = true;
			readyForNextInstruction = true;
		}

        //private void InitMovement()
        //{
        //    startTime = Time.time;
        //    targetPosition = transform.position + transform.forward * GameManager.Instance.gridSideLength;
        //}

        //private void Move()
        //{
        //    float ratio = Time.time - startTime / GameManager.Instance.robotActionSpeed;

        //    transform.position = Vector3.Lerp(startPosition, targetPosition, ratio);
        //}

        //private void InitRotation()
        //{
        //    startTime = Time.time;
        //    targetRotation = transform.rotation + transform.forward * GameManager.Instance.gridSideLength;
        //}

        //private void Rotate()
        //{
        //    float ratio = Time.time - startTime / GameManager.Instance.robotActionSpeed;

        //    transform.position = Vector3.Lerp(startPosition, targetPosition, ratio);
        //}

        IEnumerator SpinLeft(){
			canAct = false;
			for(int i = 0; i < 90; i++){
				transform.Rotate (Vector3.down, 1f);
				yield return new WaitForSeconds (Time.deltaTime);
			}
			canAct = true;
			readyForNextInstruction = true;
		}

		IEnumerator SpinRight(){
			canAct = false;
			for(int i = 0; i < 90; i++){
				transform.Rotate (Vector3.down, -1f);
				yield return new WaitForSeconds (Time.deltaTime);
			}
			canAct = true;
			readyForNextInstruction = true;
		}

        private bool ObstacleAhead(float maxDistance)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
            {
                Debug.Log("Raycast hit: " + hitInfo.transform.name);
                return true;
            }

            return false;

            //return Physics.Raycast(transform.position + transform.forward * 0.6f, transform.forward, 1);
        }
    }
}