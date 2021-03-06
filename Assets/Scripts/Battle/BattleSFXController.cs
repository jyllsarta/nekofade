﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSFXController : MonoBehaviour {

    public AudioSource selectAction;
    public AudioSource cancelAction;
    public AudioSource turnStart;

    public void playSelectAction()
    {
        selectAction.Play();
    }

    public void playCancelAction()
    {
        cancelAction.Play();
    }

    public void playTurnStart()
    {
        turnStart.Play();
    }
}
