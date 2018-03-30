using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//バトルシーンを呼び出すためのやつ
public class BattleSimulator : MonoBehaviour {

    public SirokoStats siroko;
    public SirokoStats sirokoPrefab;
    public SelectingList equips;
    public SelectingList enemies;
    public SelectingList actions;
    public SelectingList attendants;
    public SelectingList items;

    public Slider strength;
    public Slider intelligence;
    public Slider magicCapacity;
    public Slider speed;
    public Slider defence;
    public Slider vitality;

    //
    public void setParameters()
    {
        siroko.enemies = enemies.getChildContents();
        siroko.equipments = equips.getChildContents();
        siroko.actions = actions.getChildContents();
        siroko.attendants = attendants.getChildContents();
        siroko.items = items.getChildContents();
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
        siroko = FindObjectOfType<SirokoStats>();
        if (!siroko)
        {
            siroko = Instantiate<SirokoStats>(sirokoPrefab);
        }
        siroko.strength = (int)strength.value;
        siroko.intelligence = (int)intelligence.value;
        siroko.magicCapacity = (int)magicCapacity.value;
        siroko.speed = (int)speed.value;
        siroko.defence = (int)defence.value;
        siroko.vitality = (int)vitality.value;
        setDefaultBattleData();
    }

    // Update is called once per frame
    void Update () {
		
	}

    //戦闘開始
    public void startBattle()
    {
        setParameters();
        siroko.hp = siroko.vitality * 40 + 100;
        siroko.maxHp = siroko.vitality * 40 + 100;
        siroko.mp = siroko.magicCapacity * 40 + 100;
        siroko.maxMp = siroko.magicCapacity * 40 + 100;
        SceneManager.LoadSceneAsync("battleAlpha");
    }
}
