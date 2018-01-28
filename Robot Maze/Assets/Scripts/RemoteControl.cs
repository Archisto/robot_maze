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
        private GameObject arrow;

        private bool canGoDown = true;
        private bool canGoUp = false;

        private bool _buttonIsHeld = false;

        private bool resetButtonTarget = false;

		private bool speedButtonTarget = false;

		private float speedOptionTarget = 0f;

		private SpeedOption speedObjectTarget;

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
                CheckTargetResetButton(hitObj);
				CheckTargetSpeedButton (hitObj);
                CheckTargetRobot(hitObj);
				Debug.Log (hitObj.name);
			} else {
				arrow.SetActive (false);
			}
        }

        private bool CheckTargetResetButton(GameObject hitObj)
        {
            if (hitObj.tag == "ResetButton")
            {
                if (!resetButtonTarget)
                {
                    resetButtonTarget = true;
                }
                return true;
            }
            else
            {
                if (resetButtonTarget)
                {
                    resetButtonTarget = false;
                }
                return false;
            }
        }

		private bool CheckTargetSpeedButton(GameObject hitObj)
		{
			if (hitObj.tag == "Speed")
			{
				

				if (hitObj.GetComponent<SpeedOption> () != null) 
				{
					speedObjectTarget = hitObj.GetComponent<SpeedOption> ();
					speedOptionTarget = speedObjectTarget.Number;
				}

				if (!speedButtonTarget)
				{
					speedButtonTarget = true;
				}
				return true;
			}
			else
			{
				if (speedButtonTarget)
				{
					speedButtonTarget = false;
				}
				return false;
			}
		}

        private bool CheckTargetRobot(GameObject hitObj)
        {
            targetRobot = null;
            targetRobot = hitObj.GetComponent<Robot>();
			if (targetRobot != null && !targetRobot.IsBroken())
            {
                arrow.SetActive(true);
                arrow.transform.position = new Vector3(targetRobot.transform.position.x, targetRobot.transform.position.y + 2f, targetRobot.transform.position.z);

                return true;
            }
            else
            {
                arrow.SetActive(false);

                return false;
            }
        }

        public void OnBigButtonReleased()
        {
            if (resetButtonTarget)
            {
                // Plays a sound
                SFXPlayer.Instance.Play(Sound.Confirm, false);

                GameManager.Instance.ResetLevel();
            }
			else if (speedButtonTarget)
			{
                // Plays a sound
                SFXPlayer.Instance.Play(Sound.Confirm, false);

                GameManager.Instance.robotActionDuration = speedOptionTarget;
				speedObjectTarget.WasClicked ();
            }
            else
            {
                if (targetRobot != null)
                {
                    // Plays a sound
                    SFXPlayer.Instance.Play(Sound.Confirm, false);

                    Debug.Log("Robot activated");
                    //targetRobot.Active = true;
                    targetRobot.Activate(instructions);
                    targetRobot = null;
                }

                // No target
                else
                {
                    // Plays a sound
                    SFXPlayer.Instance.Play(Sound.ButtonPress, false);
                }

                arrow.SetActive(false);
            }

            _buttonIsHeld = false;
            StartCoroutine(ButtonUp());
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
