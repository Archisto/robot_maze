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

        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Quaternion startRotation;
        private Quaternion targetRotation;

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
                        active = false;
                    }
                }
                else
                {
                    FollowCurrentInstruction();
                }
            }
        }

		public void Activate(Instruction[] gotInstructions)
        {
			if (!active) {
				currentInstruction = 0;
				readyForNextInstruction = true;
				instructions = gotInstructions;
				active = true;

				startPosition = transform.position;
				startRotation = transform.rotation;
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

        private void PickUpItem()
        {

        }

        private void DropItem()
        {

        }

		public void MoveForward()
		{
			if (canAct) {
				StartCoroutine (MoveSquare ());
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
			for(int i = 0; i < 60; i++){
				transform.Translate (Vector3.forward * 1 / 60);
				yield return new WaitForSeconds (Time.deltaTime);
			}
			canAct = true;
			readyForNextInstruction = true;
		}

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
    }
}