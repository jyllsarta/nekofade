using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲーム中強化していくしろこのステータス
[System.Serializable]
public class SirokoStats : MonoBehaviour{
    //各種パラメータ
    public int strength;
    public int speed;
    public int intelligence;
    public int vitality;
    public int defence;
    public int maxHp; //hpはバトル画面以外では全回復する
    public int mp;
    public int maxMp;

    //TODO 装備
    public List<string> equipments;
    //TODO 技リスト
    public List<string> actions;
    //TODO 同行キャラ
    public List<string> attendants;

    //ちょい違うけどシーンの都合でこっちにあると嬉しい
    public List<string> enemies;


    void Start()
    {
        DontDestroyOnLoad(this);    
    }
}
