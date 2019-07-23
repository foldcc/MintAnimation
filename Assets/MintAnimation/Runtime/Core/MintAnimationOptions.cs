using UnityEngine;

namespace MintAnimation.Core
{
    [System.Serializable]
    public class MintAnimationOptions
    {
        public float                Duration = 0.35f;

        public bool                 IsBack;
        public bool                 IsLoop;
        public bool                 IsReversal;
        public int                  LoopCount = -1;
       
        public bool                 IsCustomEase = false;
        public MintEaseMethod       EaseType = MintEaseMethod.Linear;
        public AnimationCurve       TimeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public bool                 AutoStartValue = true;
        
        public DriveEnum            DriveType = DriveEnum.Globa;
        public UpdaterTypeEnum      UpdaterTypeEnum = UpdaterTypeEnum.Update;
        public MintDriveComponent   CustomDrive;
    }
}