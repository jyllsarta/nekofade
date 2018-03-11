using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バトルシーンを呼び出すためのやつ
public class BattleSimulator : MonoBehaviour {

    public SirokoStats siroko;

    public void sliderHandler(int value)
    {
        siroko.strength = value;
    }

	// Use this for initialization
	void Start () {
        siroko = new SirokoStats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
