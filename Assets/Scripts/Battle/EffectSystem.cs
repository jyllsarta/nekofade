using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour {

    public GameObject blueExplosion;
    public GameObject defaultEffect;

    //名前
    public GameObject getEffectByActionName(string name)
    {
        switch (name)
        {
            case "瞬突":
                return blueExplosion;
            default:
                return defaultEffect;
        }

    }
}
