using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFXController : MonoBehaviour
{

    public AudioSource uimenuOpen;
    public AudioSource uimenuClose;
    public AudioSource uiLevelUp;
    public AudioSource uiCoin;
    public AudioSource uiCoinGet;
    public AudioSource uiCoinGetAlot;


    public void playUIMenuOpen()
    {
        uimenuOpen.Play();
    }

    public void playUIMenuClose()
    {
        uimenuClose.Play();
    }

    public void playUILevelUp()
    {
        uiLevelUp.Play();
    }

    public void playUICoin()
    {
        uiCoin.Play();
    }

    public void playUICoinGet()
    {
        uiCoinGet.Play();
    }

    public void playUICoinGetAlot()
    {
        uiCoinGetAlot.Play();
    }

}

