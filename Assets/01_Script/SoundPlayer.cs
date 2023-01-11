using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField] public AudioMixer audioMixer;

    public Slider audioSliderMaster;
    public Slider audioSliderBGM;
    public Slider audioSliderSFX;

    private void Update()
    {
        float volume1 = audioSliderMaster.value;
        audioMixer.SetFloat("Master", volume1);
        float volume2 = audioSliderBGM.value;
        audioMixer.SetFloat("BGM", volume2);
        float volume3 = audioSliderSFX.value;
        audioMixer.SetFloat("SFX", volume3);
    }

}
