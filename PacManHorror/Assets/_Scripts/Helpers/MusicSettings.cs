using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",1);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeVolumeBacgroundMusic()
    {
        AudioManager.instance.ChangeVolume(ChangeScene.instance.currentBackgroundMusic, volumeSlider.value);
        print($"name of Volume {ChangeScene.instance.currentBackgroundMusic} : Value {volumeSlider.value}");
        Save();
    }

    public void ChangeGameVolume()
    {
        AudioManager.instance.ChangeVolume("Chase Music", volumeSlider.value);
        print($"name of Volume Chase Music : Value {volumeSlider.value}");
        Save();
    }


    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
