using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲーム中強化していくしろこのステータス
[System.Serializable]
public class SirokoStats : MonoBehaviour{
    //各種パラメータ
    public int strength;
    public int intelligence;
    public int magicCapacity;
    public int speed;
    public int defence;
    public int vitality;
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;
    //所持金
    public int gold;

    //装備
    public List<string> equipments;
    //技リスト
    public List<string> actions;
    //TODO 同行キャラ
    public List<string> attendants;
    //TODO アイテム
    public List<string> items;

    //ちょい違うけどシーンの都合でこっちにあると嬉しい
    public List<string> enemies;


    void Start()
    {
        DontDestroyOnLoad(this);
        this.maxHp = vitality * 40 + 100;
        this.maxMp = magicCapacity * 40 + 100;
    }

    public void healHp(int value)
    {
        if (hp < maxHp)
        {
            hp += value;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }
    public void healMp(int value)
    {
        if (mp < maxMp)
        {
            mp += value;
            if (mp > maxMp)
            {
                mp = maxMp;
            }
        }
    }

}
