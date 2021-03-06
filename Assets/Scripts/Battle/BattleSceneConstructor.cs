﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DontDestroyOnLoadされたパラメータリストをよみこんでバトルシーンに反映させる
public class BattleSceneConstructor : MonoBehaviour {

    public SirokoStats status;
    public Battle battleModel;
    public BattleActionsArea actionsArea;

    void setSirokoParameter()
    {
        battleModel.player.strength = status.getStrengthLevel();
        battleModel.player.intelligence = status.getIntelligenceLevel();
        battleModel.player.magicCapacity = status.getMagicCapacityLevel();
        battleModel.player.speed = status.getSpeedLevel();
        battleModel.player.vitality = status.getVitalityLevel();
        battleModel.player.defence = status.getDefenceLevel();
        battleModel.player.hp = status.hp;
        battleModel.player.maxHp = battleModel.player.getMaxHP();
        battleModel.player.mp = status.mp;
        battleModel.player.maxMp = battleModel.player.getMaxMP();
    }

    void setActions()
    {
        actionsArea.loadActions(status.actions);
    }

    void setAttendant()
    {
        //TODO ずっと後でいい ターン1で独自行動してくれる味方 味方に攻撃は飛ばない 手数が増えるだけ
    }

    void setItems()
    {
        battleModel.setItems(status.items);
    }

    void setEnemy()
    {
        battleModel.setEnemy(status.enemies);
    }

    void setEquips()
    {
        //装備とは、戦闘開始前に処理を行い「パラメータの上昇」「バフの付与」を行うアイテムである とすれば処理しやすそう
        foreach (string s in status.equipments)
        {
            Equip e = EquipStore.getEquipByName(s);
            battleModel.player.strength += e.strength;
            battleModel.player.intelligence += e.intelligence;
            battleModel.player.speed += e.speed;
            battleModel.player.vitality += e.vitality;
            battleModel.player.defence += e.defence;
            foreach (Buff.BuffID buffID in e.enchants)
            {
                battleModel.enchantBuff(buffID, ref battleModel.player);
            }
        }
    }

    public void initialize()
    {
        if (status == null)
        {
            Debug.Log("直接バトルシーンを起動したな デフォルト値を読み込みます");
            actionsArea.loadActions(battleModel.player.actions);
            battleModel.setEnemy(new List<string>() { "scp","scp","scp"});
            return;
        }
        setSirokoParameter();
        setActions();
        setAttendant();
        setEnemy();
        setEquips();
        setItems();
        battleModel.player.shieldContainer.initialize(battleModel.player.getMaxDefenceCount(),0);
        battleModel.currentTargettingEnemyHash =battleModel.getIndexOfActiveEnemy();

    }

    // Use this for initialization
    void Start () {
        status = FindObjectOfType<SirokoStats>();
        initialize();
        battleModel.setLoadFinished();
        battleModel.onTurnStart();//こっち側で設定が終わったのでバトルを開始する
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
