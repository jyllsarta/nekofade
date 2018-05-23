using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSFXController : MonoBehaviour {

    public AudioSource selectAction;

    public void playSelectAction()
    {
        selectAction.Play();
    }
}
