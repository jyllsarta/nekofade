using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DontDestroyOnLoadされたパラメータリストをよみこんでバトルシーンに反映させる
public class BattleSceneConstructor : MonoBehaviour {

    public SirokoStats status;
    public Battle battleModel;
    public BattleActionsArea actionsArea;

    void setSirokoParameter()
    {
        battleModel.player.strength = status.strength;
        battleModel.player.intelligence = status.intelligence;
        battleModel.player.speed = status.speed;
        battleModel.player.toughness = status.toughness;
        battleModel.player.defence = status.defence;
        battleModel.player.hp = battleModel.player.getMaxHP();
        battleModel.player.maxHp = battleModel.player.getMaxHP();
        battleModel.player.mp = status.mp;
    }

    void setActions()
    {
        actionsArea.loadActions(status.actions);
    }

    void setAttendant()
    {
        //TODO ずっと後でいい ターン1で独自行動してくれる味方 味方に攻撃は飛ばない 手数が増えるだけ
    }

    void setEnemy()
    {
        //EnemyStoreがいるね
    }

    void setEquips()
    {
        //装備とは、戦闘開始前に処理を行い「パラメータの上昇」「バフの付与」を行うアイテムである とすれば処理しやすそう
    }

    public void initialize()
    {
        setSirokoParameter();
        setActions();
        setAttendant();
        setEnemy();
        setEquips();
    }

	// Use this for initialization
	void Start () {
        status = FindObjectOfType<SirokoStats>();
        initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
