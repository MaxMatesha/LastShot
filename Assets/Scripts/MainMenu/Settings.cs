using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    bool isFullScreen = true;
    public AudioMixer am;
    public new AudioSource audio;
    public Slider S;
    public void FullScreenToggle()
    {
        
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        AudioListener.volume = S.value;
        if (!audio.isPlaying) audio.Play(1);
        PlayerPrefs.SetFloat("Volume", S.value);
        PlayerPrefs.Save();
    }

    public void Quality(int q)
    {
        Debug.Log(q);
        QualitySettings.SetQualityLevel(q);
    }
}
