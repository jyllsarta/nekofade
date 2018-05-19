using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour {

    public GameObject blueExplosion;
    public GameObject icyExplosion;
    public GameObject fireNova;
    public GameObject thunder;
    public GameObject swordSlash;
    public GameObject defaultEffect;
    public GameObject shieldEffect;
    public GameObject buffEffect;

    //名前
    public GameObject getEffectByActionName(string name)
    {
        switch (name)
        {
            case "瞬突":
                return blueExplosion;
            case "刺突":
                return blueExplosion;
            case "双撃":
                return swordSlash;
            case "凍結":
                return icyExplosion;
            case "雷光":
                return thunder;
            case "獄炎":
                return fireNova;
            case "防御":
                return shieldEffect;
            case "癒陣":
                return buffEffect;
            case "響火":
                return buffEffect;
            default:
                return defaultEffect;
        }

    }
}
