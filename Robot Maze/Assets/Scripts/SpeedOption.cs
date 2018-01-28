using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class SpeedOption : MonoBehaviour
    {
        [SerializeField]
        private int number;

        public int Number
        {
            get
            {
                return number;
            }
        }
    }
}
