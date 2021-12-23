using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    private readonly string _indexGameVolume = "GameVolume";
    public UnityEngine.UI.Slider Volume;

    private float GameVolume
    {
        get => PlayerPrefs.GetFloat(_indexGameVolume, 0.5f);
        set => PlayerPrefs.SetFloat(_indexGameVolume, value);
    }

    private void Awake()
    {
        AudioListener.volume = GameVolume;
        Volume.value = GameVolume;
    }
    public void VolumeUpdate(float value)
    {
        AudioListener.volume = Volume.value;
        GameVolume = Volume.value;
    }
}
