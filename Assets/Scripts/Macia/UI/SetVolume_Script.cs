using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume_Script : MonoBehaviour
{
    [SerializeField] AudioMixer masterAudioMixer; 


    public void SetMusicVolume(float musicVolume)
    {
        masterAudioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20); //SOUND SCALE IS LOGARITHMIC!
    }

    public void SetFXVolume(float fxVolume)
    {
        masterAudioMixer.SetFloat("fxVolume", Mathf.Log10(fxVolume) * 20);
    }

}
