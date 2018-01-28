using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class SpeedOption : MonoBehaviour
    {
        [SerializeField]
        private float number;

		[SerializeField]
		private Material[] materials;

		private SpeedOption[] options;

		public float Number
        {
            get
            {
                return number;
            }
        }

		void Start()
		{
			options = FindObjectsOfType<SpeedOption> ();
		}

		public void WasClicked()
		{
			foreach (SpeedOption op in options) {
				op.gameObject.GetComponent<MeshRenderer> ().material = materials [0];
			}

			GetComponent<MeshRenderer> ().material = materials [1];
		}
    }
}
