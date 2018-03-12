using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//バトルシーンを呼び出すためのやつ
public class BattleSimulator : MonoBehaviour {

    public SirokoStats siroko;
    public SelectingList equips;
    public SelectingList enemies;
    public SelectingList actions;
    public SelectingList attendants;

    public Slider strength;
    public Slider toughness;
    public Slider intelligence;
    public Slider speed;
    public Slider defence;

    //
    public void setParameters()
    {
        siroko.enemies = enemies.getChildContents();
        siroko.equipments = equips.getChildContents();
        siroko.actions = actions.getChildContents();
        siroko.attendants = attendants.getChildContents();
    }

	// Use this for initialization
	void Start () {
        siroko.strength = (int)strength.value;
        siroko.toughness = (int)toughness.value;
        siroko.speed = (int)speed.value;
        siroko.defence = (int)defence.value;
        siroko.intelligence = (int)intelligence.value;
        siroko.maxMp = 1000;
        siroko.mp = 1000;
    }

    // Update is called once per frame
    void Update () {
		
	}

    //戦闘開始
    public void startBattle()
    {
        setParameters();
        SceneManager.LoadScene("battleAlpha");
    }
}
