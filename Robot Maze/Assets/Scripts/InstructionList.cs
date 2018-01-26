﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class InstructionList : MonoBehaviour
    {
        private Instruction[] instructions;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start()
        {
            instructions = GetComponentsInChildren<Instruction>();
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {

        }

        public Instruction[] Instructions
        {
            get
            {
                return instructions;
            }
        }
    }
}
