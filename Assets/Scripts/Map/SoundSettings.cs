using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour {

    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private GameObject sliders;

    [SerializeField]
    private AudioMixer audioMixer;

    public void showSoundSettings()
    {
        sliders.gameObject.SetActive(true);
    }

    public void hideSoundSettings()
    {
        sliders.gameObject.SetActive(false);
    }

    public void updateBGMVolume()
    {
        audioMixer.SetFloat("BGMVolume", bgmSlider.value <= -30f ? -80f: bgmSlider.value);
    }
    public void updateSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxSlider.value <= -30f ? -80f : sfxSlider.value);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       var amg  = audioMixer.FindMatchingGroups("BGM");
	}
}
