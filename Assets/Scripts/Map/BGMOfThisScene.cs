using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMOfThisScene : MonoBehaviour {

    [SerializeField]
    private AudioSource battle;
    [SerializeField]
    private AudioSource boss;
    [SerializeField]
    private AudioSource field;

    [SerializeField]
    private float changeSpeed;

    private AudioSource currentMusic;
    private AudioSource prevMusic;

    // Use this for initialization
    void Start () {
        field.Play();
        changeMusic(battle, field);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "battleAlpha":
                changeMusic(field, battle);
                break;
        }
    }

    void OnSceneUnLoaded(Scene scene)
    {
        switch (scene.name)
        {
            case "battleAlpha":
                changeMusic(battle, field);
                break;

        }
    }

    void changeMusic(AudioSource prev, AudioSource next)
    {
        prevMusic = prev;
        currentMusic = next;
        currentMusic.volume = 0f;
        if (currentMusic.time <= 1f)
        {
            currentMusic.Play();
        }
        else
        {
            currentMusic.UnPause();
        }
    }

    private void Update()
    {
        if (currentMusic.volume < 0.95f)
        {
            currentMusic.volume += changeSpeed;
        }
        if (prevMusic.volume > 0f)
        {
            prevMusic.volume -= changeSpeed;
        }

        if (prevMusic.volume <= changeSpeed)
        {
            prevMusic.Pause();
        }

    }

}
