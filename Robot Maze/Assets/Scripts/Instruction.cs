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
        PushItem = 4,
        PickUpItem = 5,
        DropItem = 6,
        Stop = 7
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
