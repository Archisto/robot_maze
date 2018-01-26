using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
	
	public class GoDown : MonoBehaviour 
	{

		bool goingDown = false;

		void Update()
		{
			if (goingDown) 
			{
				transform.Translate (Vector3.down * Time.deltaTime * 0.1f);
				if (transform.localPosition.y <= 0.05f) {
					transform.localPosition = new Vector3 (transform.localPosition.x, 0.05f, transform.localPosition.z);
					goingDown = false;
				}
			}
		}

		public void BePressed()
		{
			goingDown = true;
			//open door
		}
	}
}
