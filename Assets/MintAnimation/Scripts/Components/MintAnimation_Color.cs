using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MintAnimation
{
    public class MintAnimation_Color : MintAnimation_Base
    {
        private Color               mGetColor;
        private Graphic             mGrahic;
        private Material            mMaterail;

        private MintAnimationClip<Color> mMintAnimationClip;

        private void OnEnable()
        {
            init();
        }

        private void OnDisable()
        {
            mMintAnimationClip?.Stop();
        }

        private void init()
        {
            this.mGrahic = this.gameObject.GetComponent<Graphic>();
            var m = this.gameObject.GetComponent<MeshRenderer>();
            if (m != null) this.mMaterail = m.material;

            AnimationInfo.AutoStartValue = false;
            mMintAnimationClip = new MintAnimationClip<Color>(getter , setter , AnimationInfo);
            if (IsAutoPlay)
            {
                mMintAnimationClip.Play();
            }
        }

        private Color getter()
        {
            return mGetColor;
        }

        private void setter(Color rColor)
        {
            this.mGetColor = rColor;

            if (this.mGrahic != null)
            {
                this.mGrahic.color = rColor;
            }
            if (this.mMaterail != null)
            {
                this.mMaterail.color = rColor;
            }
        }
    }
}
