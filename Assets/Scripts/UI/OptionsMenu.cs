using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetMaster(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetMusic(float volume)
    {
        audioMixer.SetFloat ("Music", volume);
    }

    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
