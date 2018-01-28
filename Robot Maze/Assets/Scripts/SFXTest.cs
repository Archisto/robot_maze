using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotMaze
{
    public class SFXTest : MonoBehaviour
    {
        private AudioSource loopingSound;
        public bool playLoop;
        public bool playLoop2;
        public bool playLoop3;
        public bool playLoop4;
        public bool endLoop;

        public bool play;
        public bool play2;
        public bool play3;
        public bool play4;
        public bool play5;
        public bool play6;
        public bool play7;
        private void Update()
        {
            if (playLoop)
            {
                loopingSound = SFXPlayer.Instance.Play(Sound.Engine1, true);
                playLoop = false;
            }
            if (playLoop2)
            {
                loopingSound = SFXPlayer.Instance.Play(Sound.Engine2, true);
                playLoop2 = false;
            }
            if (playLoop3)
            {
                loopingSound = SFXPlayer.Instance.Play(Sound.MainEngine, true);
                playLoop3 = false;
            }
            if (playLoop4)
            {
                loopingSound = SFXPlayer.Instance.Play(Sound.Engine4, true);
                playLoop4 = false;
            }
            if (endLoop)
            {
                if (loopingSound != null)
                {
                    loopingSound.Stop();
                    loopingSound.loop = false;
                    endLoop = false;
                }
            }

            if (play)
            {
                SFXPlayer.Instance.Play(Sound.Chirp, false);
                play = false;
            }
            if (play2)
            {
                SFXPlayer.Instance.Play(Sound.Chatter, false);
                play2 = false;
            }
            if (play3)
            {
                SFXPlayer.Instance.Play(Sound.Confirm, false);
                play3 = false;
            }
            if (play4)
            {
                SFXPlayer.Instance.Play(Sound.ButtonPress, false);
                play4 = false;
            }
            if (play5)
            {
                SFXPlayer.Instance.Play(Sound.RadioShort, false);
                play5 = false;
            }
            if (play6)
            {
                SFXPlayer.Instance.Play(Sound.RadioRising, false);
                play6 = false;
            }
            //
        }
    }
}