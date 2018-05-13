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

    //かけた時間
    public int clock;

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

    [System.Serializable]
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
        refreshParameters();
        clock = 0;
    }

    public void refreshParameters()
    {
        this.maxHp = vitality * 40 + 100;
        this.maxMp = magicCapacity * 40 + 100;
    }

    public void addAction(string name)
    {
        actions.Add(name);
    }
    public void addEquip(string name)
    {
        equipments.Add(name);
    }
    public void addItem(string name)
    {
        items.Add(name);
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
        return (level + 2) * 50;
    }

    public int getLevel(ParameterKind kind)
    {
        switch (kind)
        {
            case ParameterKind.STRENGTH:
                return strength;
            case ParameterKind.INTELLIGENCE:
                return intelligence;
            case ParameterKind.MAGICCAPACITY:
                return magicCapacity;
            case ParameterKind.SPEED:
                return speed;
            case ParameterKind.DEFENCE:
                return defence;
            case ParameterKind.VITALITY:
                return vitality;
        }
        return 0;
    }

    public bool canLevelUp(ParameterKind kind)
    {
        return getLevel(kind) < 5 && getLevelupCost(kind) <= gold;
    }

    public void levelUp(ParameterKind kind)
    {
        int cost = getLevelupCost(kind);
        if (cost > gold)
        {
            Debug.Log("払えないよ");
            return;
        }
        if (getLevel(kind) >= 5)
        {
            Debug.Log("もうマックスだよ");
            return;
        }
        gold -= cost;

        switch (kind)
        {
            case ParameterKind.STRENGTH:
                strength++;
                break;
            case ParameterKind.INTELLIGENCE:
                intelligence++;
                break;
            case ParameterKind.MAGICCAPACITY:
                magicCapacity++;
                break;
            case ParameterKind.SPEED:
                speed++;
                break;
            case ParameterKind.DEFENCE:
                defence++;
                break;
            case ParameterKind.VITALITY:
                vitality++;
                break;
        }
        //パラメータ直接上昇系のやつは増えた分を即時反映
        if (kind == ParameterKind.MAGICCAPACITY)
        {
            refreshParameters();
            healMp(40);
        }
        if (kind == ParameterKind.VITALITY)
        {
            refreshParameters();
            healHp(40);
        }

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

    public string getLevelupText(ParameterKind kind)
    {
        switch (kind)
        {
            case ParameterKind.STRENGTH:
                return string.Format("物理攻撃のダメージ倍率{0}%→{1}%",100+strength*40, 100 + (strength+1) * 40);
            case ParameterKind.INTELLIGENCE:
                return string.Format("魔法攻撃のダメージ倍率{0}%→{1}%", 100 + intelligence * 40, 100 + (intelligence + 1) * 40);
            case ParameterKind.MAGICCAPACITY:
                return string.Format("最大MP{0}→{1}, Lv3以上でターン終了時のMP回復量UP", 100 + magicCapacity * 40, 100 + (magicCapacity + 1) * 40);
            case ParameterKind.SPEED:
                return string.Format("すべての行動のWTを{0}%→{1}%カット", 100-100*BattleCharacter.getDefaultWaitTimeCutRate(speed),100- 100 * BattleCharacter.getDefaultWaitTimeCutRate(speed+1));
            case ParameterKind.DEFENCE:
                return string.Format("物理ダメージカット{0}%→{1}%,防御時さらに{2}%→{3}%のダメージを軽減",
                    100 * BattleCharacter.getDefaultNormalCutRate(defence),
                    100 * BattleCharacter.getDefaultNormalCutRate(defence+1),
                    100 * BattleCharacter.getDefaultDefenceCutRate(defence),
                    100 * BattleCharacter.getDefaultDefenceCutRate(defence+1)
                    );
            case ParameterKind.VITALITY:
                return string.Format("最大HP{0}→{1}", 100 + vitality * 40, 100 + (vitality + 1) * 40);
            default:
                return "";
        }
    }

    public void tick()
    {
        clock++;
    }
    public void addTurnCount(int count)
    {
        clock += count;
    }

    public void buy(StoreAreaComponent.ItemKind kind, string itemName, int cost)
    {
        if (cost > gold)
        {
            Debug.LogWarning("お金が足りないよ(今後これが出ないように制御しよう)");
            return;
        }
        gold -= cost;
        switch (kind)
        {
            case StoreAreaComponent.ItemKind.ACTION:
                actions.Add(itemName);
                break;
            case StoreAreaComponent.ItemKind.EQUIP:
                equipments.Add(itemName);
                break;
            case StoreAreaComponent.ItemKind.ITEM:
                items.Add(itemName);
                break;
        }
    }

}
