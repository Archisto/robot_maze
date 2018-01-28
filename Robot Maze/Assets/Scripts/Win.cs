using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{

	public class Win : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerEnter(Collider other){
			if (other.gameObject.tag == "Robot") {
				Robot[] robots = FindObjectsOfType<Robot> ();
				foreach (Robot rob in robots) {
					rob.enabled = false;
				}
			}
		}
	}
}
