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
    public Slider vitality;
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

    void setDefaultBattleData()
    {
        actions.addChild("刺突", "説明はいいや適当で");
        actions.addChild("防御", "説明はいいや適当で");
        actions.addChild("癒陣", "説明はいいや適当で");
        actions.addChild("火炎", "説明はいいや適当で");
        actions.addChild("重撃", "説明はいいや適当で");
        actions.addChild("雷光", "説明はいいや適当で");
        enemies.addChild("scp", "説明はいいや適当で");
        enemies.addChild("scp", "説明はいいや適当で");
        enemies.addChild("scp", "説明はいいや適当で");
        equips.addChild("プレートアーマー", "説明はいいや適当で");
    }

    // Use this for initialization
    void Start () {
        siroko.strength = (int)strength.value;
        siroko.vitality = (int)vitality.value;
        siroko.speed = (int)speed.value;
        siroko.defence = (int)defence.value;
        siroko.intelligence = (int)intelligence.value;
        siroko.maxMp = 1000;
        siroko.mp = 1000;
        setDefaultBattleData();
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
