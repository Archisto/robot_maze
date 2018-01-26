using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class Robot : MonoBehaviour
    {
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
                    Activate();
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
            if (Active)
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
                        Active = false;
                    }
                }
                else
                {
                    FollowCurrentInstruction();
                }
            }
        }

        private void Activate()
        {
            currentInstruction = 0;
            readyForNextInstruction = false;

            startPosition = transform.position;
            startRotation = transform.rotation;
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
                    TurnLeft();
                    break;
                }
                case InstructionType.TurnRight:
                {
                    TurnRight();
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

        private void MoveForward()
        {

        }

        private void TurnLeft()
        {

        }

        private void TurnRight()
        {

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
    }
}