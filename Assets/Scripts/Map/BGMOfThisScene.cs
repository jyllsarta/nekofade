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

    // Use this for initialization
    void Start () {
        field.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "battleAlpha":
                field.Pause();
                battle.Play();
                break;
        }
    }

    void OnSceneUnLoaded(Scene scene)
    {
        switch (scene.name)
        {
            case "battleAlpha":
                field.UnPause();
                break;

        }
    }

}
