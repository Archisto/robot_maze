using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class RemoteControl : MonoBehaviour
    {
        public float maxTransmissionDist;
        public Robot targetRobot;

		bool canGoDown = true;
		bool canGoUp = false;

		bool _buttonIsHeld = false;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start()
        {

        }

		private void Update()
		{
			if (_buttonIsHeld) 
			{
				//Debug.Log ("Button is held");
				OnBigButtonHeld ();
			}
		}

		public void OnBigButtonPressed()
		{
			Debug.Log ("Button is pressed");
			_buttonIsHeld = true;
		}

        public void OnBigButtonHeld()
        {
            GameObject hitObj = null;
            RaycastHit hit;
			Ray forwardRay = new Ray(transform.position + transform.up, transform.up * 100f);
			Debug.DrawRay (transform.position + transform.up, transform.up * 100f, Color.red);
            if (Physics.Raycast(forwardRay, out hit, maxTransmissionDist))
            {
                hitObj = hit.transform.gameObject;
                targetRobot = hitObj.GetComponent<Robot>();
				if (targetRobot != null) {
					Debug.Log ("That is a robot");
				}
				Debug.Log (hitObj.name);
            }
        }

        public void OnBigButtonReleased()
        {
            if (targetRobot != null)
            {
                Debug.Log("Robot activated");
                //targetRobot.Active = true;
				targetRobot.MoveForward();
                targetRobot = null;
            }
			_buttonIsHeld = false;
        }

		public void ButtonDown(){

		}
    }
}
