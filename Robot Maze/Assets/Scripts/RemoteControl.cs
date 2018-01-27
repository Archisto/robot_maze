using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class RemoteControl : MonoBehaviour
    {
		private Instruction[] instructions;

        [SerializeField]
        private float maxTransmissionDist;

        [SerializeField]
        private Robot targetRobot;

		[SerializeField]
        private Transform button;

		[SerializeField]
		GameObject arrow;

		bool canGoDown = true;
		bool canGoUp = false;

		bool _buttonIsHeld = false;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start()
        {
			instructions = FindObjectOfType<InstructionList> ().Instructions;
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
			StartCoroutine (ButtonDown ());
		}

        public void OnBigButtonHeld()
        {
            GameObject hitObj = null;
            RaycastHit hit;
			Ray forwardRay = new Ray(transform.position + transform.up, transform.up * 100f);
			//Debug.DrawRay (transform.position + transform.up, transform.up * 100f, Color.red);
			if (Physics.Raycast (forwardRay, out hit, maxTransmissionDist)) {
				hitObj = hit.transform.gameObject;
				targetRobot = null;
				targetRobot = hitObj.GetComponent<Robot> ();
				if (targetRobot != null) {
					Debug.Log ("That is a robot");
					arrow.SetActive (true);
					arrow.transform.position = new Vector3 (targetRobot.transform.position.x, targetRobot.transform.position.y + 2f, targetRobot.transform.position.z);
				} else {
					arrow.SetActive (false);
				}
				Debug.Log (hitObj.name);
			} else {
				arrow.SetActive (false);
			}
        }

        public void OnBigButtonReleased()
        {
            if (targetRobot != null)
            {
                Debug.Log("Robot activated");
                //targetRobot.Active = true;
				targetRobot.Activate(instructions);
                targetRobot = null;
            }
			_buttonIsHeld = false;
			arrow.SetActive (false);
			StartCoroutine (ButtonUp ());
        }

		IEnumerator ButtonDown(){
			//up z-coordinate is [0.05]
			//down z-coordinate is [0.04]

			while (!canGoDown) {
				yield return new WaitForSeconds (Time.deltaTime);
			}

			canGoDown = false;

			for (int i = 0; i < 5; i++) {
				button.Translate (0f, 0f, -0.0008f);
				yield return new WaitForSeconds (Time.deltaTime);
			}

			canGoUp = true;
		}

		IEnumerator ButtonUp(){
			//up z-coordinate is [0.05]
			//down z-coordinate is [0.04]

			while (!canGoUp) {
				yield return new WaitForSeconds (Time.deltaTime);
			}

			canGoUp = false;

			for (int i = 0; i < 5; i++) {
				button.Translate (0f, 0f, 0.0008f);
				yield return new WaitForSeconds (Time.deltaTime);
			}

			canGoDown = true;
		}
    }
}
