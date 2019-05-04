using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation {
    public class MintDriveComponentSinge : MintDriveComponent
    {
        private static MintDriveComponentSinge _instance;
        public static MintDriveComponentSinge Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectsOfType<MintDriveComponentSinge>();
                    for (int i = 0; i < objs.Length; i++)
                    {
                        Destroy(objs[i]);
                    }
                    _instance = new GameObject().AddComponent<MintDriveComponentSinge>();
                    _instance.name = "[ MintAnimationDrive ]";
                    DontDestroyOnLoad(_instance);
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


