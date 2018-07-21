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
    public GameObject hibihiEffect;
    public GameObject fireEffect;
    public GameObject hitEffect;
    public GameObject electricEffect;
    public GameObject massFleeze;

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
                return massFleeze;
            case "雷光":
                return thunder;
            case "獄炎":
                return fireNova;
            case "防御":
                return shieldEffect;
            case "癒陣":
                return buffEffect;
            case "響火":
                return hibihiEffect;
            case "火炎":
                return fireEffect;
            case "爪":
                return swordSlash;
            case "するどい爪":
                return swordSlash;
            case "プチ火炎":
                return fireEffect;
            case "二重障壁":
                return shieldEffect;
            case "障壁展開":
                return shieldEffect;
            case "てしてし":
                return swordSlash;
            case "静電気":
                return electricEffect;
            case "打撃":
                return hitEffect;
            case "大凍結":
                return massFleeze;
            default:
                return defaultEffect;
        }

    }
}
