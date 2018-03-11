﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageEffect : MonoBehaviour {

    public int lifetime;
    public TextMeshProUGUI damageText;

	// Use this for initialization
	void Start () {
        lifetime = 180;
	}
	
	// Update is called once per frame
	void Update () {

        damageText.alpha = (float)lifetime / 60;
        damageText.transform.Translate(new Vector3(0, 1, 0));

        lifetime--;
        if (lifetime == 0)
        {
            Destroy(gameObject);
        }
	}
}
