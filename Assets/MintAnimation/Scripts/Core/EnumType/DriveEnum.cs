using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MintAnimation {

    public enum DriveEnum {
        //自定义驱动器
        Custom,
        //MintDriveComponentSinge
        Globa
    }

    public enum UpdaterTypeEnum
    {
        Update,
        FixedUpdate,
        Coroutine
    }
}

