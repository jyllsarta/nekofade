using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲーム中強化していくしろこのステータス
[System.Serializable]
public class SirokoStats{
    //各種パラメータ
    public int strength;
    public int speed;
    public int intelligence;
    public int toughness;
    public int defence;
    public int maxHp; //hpはバトル画面以外では全回復する
    public int mp;
    public int maxMp;

    //TODO 装備

    //TODO 技リスト

}
