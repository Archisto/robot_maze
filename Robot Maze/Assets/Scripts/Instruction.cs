using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public enum InstructionType
    {
        MoveForward = 0,
        TurnLeft = 1,
        TurnRight = 2,
        UTurn = 3,
        PickUpItem = 4,
        DropItem = 5,
        Stop = 6
    }

    public class Instruction : MonoBehaviour
    {
        public InstructionType type;
        public string text;

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

        }
    }
}
