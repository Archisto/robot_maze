using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class SFXTest : MonoBehaviour
    {
        public bool play;
        private void Update()
        {
            if (play)
            {
                SFXPlayer.Instance.Play(Sound.Engine1, true);
                play = false;
            }
        }
    }
}