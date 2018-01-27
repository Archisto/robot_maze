using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class RobotCollision : MonoBehaviour
    {
        public LayerMask layerMask;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start()
        {

        }

        public bool Hit(Vector3 position)
        {
            RaycastHit hit;
            return Physics.SphereCast(position, 0.1f, Vector3.zero, out hit);
        }
    }
}
