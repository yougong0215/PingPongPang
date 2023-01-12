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
        audioMixer.SetFloat("Master", Mathf.Lerp(-15, 0, sliderVal));

        float t;
        if(sliderVal == 0)
        {
            audioMixer.SetFloat("Master", -80);
        }


        audioMixer.GetFloat("Master", out t);

        Debug.Log(t);
    }
    public void SetLevelBGM(float sliderVal)
    {
        audioMixer.SetFloat("BGM", Mathf.Lerp(-15, 0, sliderVal));
        if (sliderVal == 0)
        {
            audioMixer.SetFloat("BGM", -80);
        }
    }
    public void SetLevelSound(float sliderVal)
    {
        audioMixer.SetFloat("SFX", Mathf.Lerp(-15, 0, sliderVal));
        if (sliderVal == 0)
        {
            audioMixer.SetFloat("SFX", -80);
        }
    }

}
