using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation.Core
{
    public class MintDriveComponentSinge : MintDriveComponent
    {
        private static bool mIsInit = false;
        private static MintDriveComponentSinge _instance;
        public static MintDriveComponentSinge Instance
        {
            get
            {
                if (_instance == null && !mIsInit)
                {
                    var objs = FindObjectsOfType<MintDriveComponentSinge>();
                    for (int i = 0; i < objs.Length; i++)
                    {
                        Destroy(objs[i]);
                    }
                    _instance = new GameObject().AddComponent<MintDriveComponentSinge>();
                    _instance.name = "[ MintAnimationDrive ]";
                    DontDestroyOnLoad(_instance);
                    mIsInit = true;
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

    }
}


