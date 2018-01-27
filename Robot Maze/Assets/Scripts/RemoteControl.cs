using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class RemoteControl : MonoBehaviour
    {
        public float maxTransmissionDist;
        public Robot targetRobot;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start()
        {

        }

        public void OnBigButtonHeld()
        {
            GameObject hitObj = null;
            RaycastHit hit;
            Ray forwardRay = new Ray(transform.position, transform.up);
            if (Physics.Raycast(forwardRay, out hit, maxTransmissionDist))
            {
                hitObj = hit.transform.gameObject;
                targetRobot = hitObj.GetComponent<Robot>();
            }
        }

        public void OnBigButtonReleased()
        {
            if (targetRobot != null)
            {
                Debug.Log("Robot activated");
                //targetRobot.Active = true;

                targetRobot = null;
            }
        }
    }
}
