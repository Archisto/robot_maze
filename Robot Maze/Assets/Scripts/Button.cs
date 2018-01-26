using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class Button : MonoBehaviour
    {

		GoDown _buttonCylinder;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start()
        {
			_buttonCylinder = transform.parent.Find ("Cylinder").GetComponent<GoDown>();
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {

        }

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Robot") 
			{
				_buttonCylinder.BePressed ();
			}
		}
    }
}
