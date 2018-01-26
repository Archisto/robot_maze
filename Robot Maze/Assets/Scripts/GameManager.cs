using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RobotMaze
{
    public class GameManager : MonoBehaviour
    {
        #region Statics
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Note:
                    // There must be a Resources folder under Assets and
                    // GameManager there for this to work. Not necessary if
                    // a GameManager object is present in a scene from the
                    // get-go.

                    instance =
                        Instantiate(Resources.Load<GameManager>("GameManager"));
                }

                return instance;
            }
        }
        #endregion Statics

        public float gridSideLength;
        public float robotMoveDuration;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Init();
        }

        private void Update()
        {

        }

        private void Init()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void ResetLevel()
        {
            
        }
    }
}
