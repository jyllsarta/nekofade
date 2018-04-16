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

    public enum ParameterKind{
        STRENGTH,
        INTELLIGENCE,
        MAGICCAPACITY,
        SPEED,
        DEFENCE,
        VITALITY,

    }

    void Start()
    {
        DontDestroyOnLoad(this);
        this.maxHp = vitality * 40 + 100;
        this.maxMp = magicCapacity * 40 + 100;
    }

    public int getStrengthLevel()
    {
        int total = strength;
        foreach (string s in equipments)
        {
            Equip e = EquipStore.getEquipByName(s);
            total += e.strength;
        }
        return total;
    }
    public int getIntelligenceLevel()
    {
        int total = intelligence;
        foreach (string s in equipments)
        {
            Equip e = EquipStore.getEquipByName(s);
            total += e.intelligence;
        }
        return total;
    }
    public int getMagicCapacityLevel()
    {
        int total = magicCapacity;
        foreach (string s in equipments)
        {
            Equip e = EquipStore.getEquipByName(s);
            total += e.magicCapacity;
        }
        return total;
    }
    public int getSpeedLevel()
    {
        int totalSpeed = speed;
        foreach (string s in equipments)
        {
            Equip e = EquipStore.getEquipByName(s);
            totalSpeed += e.speed;
        }
        return totalSpeed;
    }
    public int getDefenceLevel()
    {
        int total = defence;
        foreach (string s in equipments)
        {
            Equip e = EquipStore.getEquipByName(s);
            total += e.defence;
        }
        return total;
    }
    public int getVitalityLevel()
    {
        int total = vitality;
        foreach (string s in equipments)
        {
            Equip e = EquipStore.getEquipByName(s);
            total += e.vitality;
        }
        return total;
    }

    public int getLevelupCost(ParameterKind kind)
    {
        int level = 0;
        switch (kind)
        {
            case ParameterKind.STRENGTH:
                level = strength;
                break;
            case ParameterKind.INTELLIGENCE:
                level = intelligence;
                break;
            case ParameterKind.MAGICCAPACITY:
                level = magicCapacity;
                break;
            case ParameterKind.SPEED:
                level = speed;
                break;
            case ParameterKind.DEFENCE:
                level = defence;
                break;
            case ParameterKind.VITALITY:
                level = vitality;
                break;
        }
        return (level+2) * 50;
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

    public int getTotalLevel()
    {
        return strength + intelligence + magicCapacity + speed + defence + vitality;
    }

}
