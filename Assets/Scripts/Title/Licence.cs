using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Licence : MonoBehaviour {
    public void unload()
    {
        SceneManager.UnloadSceneAsync("license");
    }
}
