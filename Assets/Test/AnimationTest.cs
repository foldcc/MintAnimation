using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MintAnimation;

public class AnimationTest : MonoBehaviour
{
    public MintAnimationClip<float> mintAnimation;
    // Start is called before the first frame update
    public float value = 0;
    public float timev = 5;
    public Text asd;
    public Slider slider;

    private void OnEnable()
    {
        value = 0;
        mintAnimation = MintAnimationClip<float>.Create(() => value, (x) => { value = x; asd.text = x + ""; }, 10000, timev);
        mintAnimation.Play();
        var sa = MintAnimationClip<float>.Create(() => slider.value , (x) => slider.value = x , 1, 2.5f);
        sa.Play();
    }
}
