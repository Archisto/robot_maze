using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace RobotMaze
{
	public class MyInputHandler : MonoBehaviour {

		[SerializeField]
		RemoteControl _myRC;

		// Use this for initialization
		void Start () {
			if (GetComponent<VRTK_ControllerEvents>() == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
				return;
			}


			GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);
			GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
		{
			VRTK_Logger.Info("Controller on index '" + index + "' " + button + " has been " + action
				+ " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
		}

		private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
		{
			DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "pressed down", e);
			if (_myRC != null) 
			{
				_myRC.OnBigButtonPressed ();
			}
		}

		private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
		{
			DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "released", e);
			if (_myRC != null) 
			{
				_myRC.OnBigButtonReleased ();
			}
		}
	}
}
