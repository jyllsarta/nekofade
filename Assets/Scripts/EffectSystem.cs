using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour {

    public GameObject hitEffect;

    //名前
    public void playEffectByName(string name, Transform parent)
    {
        switch (name)
        {
            case "hit":
                Instantiate(hitEffect,parent);
                break;
            default:
                Instantiate(hitEffect, parent);
                break;
        }

    }
}
