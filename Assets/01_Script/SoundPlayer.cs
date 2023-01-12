using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField] public AudioMixer audioMixer;

    public void SetLevelMaster(float sliderVal)
    {
        audioMixer.SetFloat("Master", Mathf.Lerp(-5, 0, sliderVal));

        float t;
        audioMixer.GetFloat("Master", out t);

        Debug.Log(t);
    }
    public void SetLevelBGM(float sliderVal)
    {
        audioMixer.SetFloat("BGM", Mathf.Lerp(-5, 5, sliderVal));
    }
    public void SetLevelSound(float sliderVal)
    {
        audioMixer.SetFloat("SFX", Mathf.Lerp(-5, 5, sliderVal));
    }

}
