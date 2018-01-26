using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class Robot : MonoBehaviour
    {

		bool canAct = true;
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
			if (Input.GetKeyDown (KeyCode.Space)) {
				MoveForward ();
			} else if (Input.GetKeyDown (KeyCode.LeftControl)) {
				RotateLeft ();
			} else if (Input.GetKeyDown (KeyCode.LeftShift)) {
				RotateRight ();
			}
        }

		public void MoveForward()
		{
			if (canAct) {
				StartCoroutine (MoveSquare ());
			}
		}

		public void RotateLeft(){
			if (canAct) {
				StartCoroutine (SpinLeft ());
			}
		}

		public void RotateRight(){
			if (canAct) {
				StartCoroutine (SpinRight ());
			}
		}

		IEnumerator MoveSquare(){
			canAct = false;
			for(int i = 0; i < 60; i++){
				transform.Translate (Vector3.forward * 1 / 60);
				yield return new WaitForSeconds (Time.deltaTime);
			}
			canAct = true;
		}

		IEnumerator SpinLeft(){
			canAct = false;
			for(int i = 0; i < 90; i++){
				transform.Rotate (Vector3.down, 1f);
				yield return new WaitForSeconds (Time.deltaTime);
			}
			canAct = true;
		}

		IEnumerator SpinRight(){
			canAct = false;
			for(int i = 0; i < 90; i++){
				transform.Rotate (Vector3.down, -1f);
				yield return new WaitForSeconds (Time.deltaTime);
			}
			canAct = true;
		}
    }
}