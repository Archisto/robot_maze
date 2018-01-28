using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{

	public class Win : MonoBehaviour {

		GameObject _myParticleSystem;

		void Start()
		{
			_myParticleSystem = transform.GetChild (0).gameObject;
		}

		public void DoWin()
		{
			Robot[] robots = FindObjectsOfType<Robot> ();
			foreach (Robot rob in robots) {
				rob.enabled = false;
			}
			SFXPlayer.Instance.Play (Sound.Chime, false);
			_myParticleSystem.SetActive (true);
		}
	}
}
