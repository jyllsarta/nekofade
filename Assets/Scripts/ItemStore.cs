using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStore : MonoBehaviour{

    public BattleItem itemPrefab;
    public Battle battle;
    public MessageArea messageArea;

    public BattleItem instanciateItemByName(string name, Transform parent)
    {
        BattleItem createdChild = Instantiate(itemPrefab, parent);
        List<Effect> effects = new List<Effect>();
        createdChild.battle = battle;
        createdChild.messageArea = messageArea;

        switch (name)
        {
            case "復活の御魂": //こいつはだけパッシブ発動で特別処理書くのでいいかもな
                createdChild.itemName = "復活の御魂";
                createdChild.description = "【復活の御魂】HPが0になるダメージを受けたときに自動発動。HP全快で復活する。";
                createdChild.setImage("UI_Icon_Infinite");
                createdChild.setCount(2);
                createdChild.isPassiveItem = true;
                return createdChild;
            case "HPポーション":
                createdChild.itemName = "HPポーション";
                createdChild.description = "【HPポーション】HPを50回復する。HP上限を超えて回復できる。";
                createdChild.setImage("UI_Icon_Trophy");
                createdChild.setCount(5);
                createdChild.isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 50, Effect.EffectType.CONSTANTEXCEEDHEAL));
                createdChild.action = new Action("HPポーション", "stub", 0, 0, effects);
                return createdChild;
            case "MPポーション":
                createdChild.itemName = "MPポーション";
                createdChild.description = "【MPポーション】MPを50回復する。MP上限を超えて回復できる。";
                createdChild.setImage("UI_Icon_FaithIslam");
                createdChild.setCount(5);
                createdChild.isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 50, Effect.EffectType.CONSTANTEXCEEDMPHEAL));
                createdChild.action = new Action("MPポーション", "stub", 0, 0, effects);
                return createdChild;
            case "DCS":
                createdChild.itemName = "魔ポテトのスープ";
                createdChild.description = "【魔ポテトのスープ】HPを40回復し、200Fの間スープ状態になる。筋力Lv+3,速度Lv+3。";
                createdChild.setImage("UI_Icon_FaithIslam");
                createdChild.setCount(1);
                createdChild.isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 40, Effect.EffectType.CONSTANTHEAL));
                effects.Add(new Effect(Effect.TargetType.ME, 99, Buff.BuffID.DCS));
                createdChild.action = new Action("魔ポテ", "stub", 0, 0, effects);
                return createdChild;
            case "雷光の符":
                createdChild.itemName = "雷光の符";
                createdChild.description = "【雷光の符】雷光を発動。ランダムな敵に合計4回魔法ダメージ。";
                createdChild.setImage("UI_Icon_Energy");
                createdChild.setCount(4);
                createdChild.isPassiveItem = false;
                createdChild.action = ActionStore.getActionByName("雷光");
                return createdChild;
            case "無敵バリア":
                createdChild.itemName = "無敵バリア";
                createdChild.description = "【無敵バリア】60Fの間、敵からのダメージをすべて無効化するバリアを展開。";
                createdChild.setImage("UI_Icon_Star");
                createdChild.setCount(2);
                createdChild.isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 99, Buff.BuffID.INVINCIBLE));
                createdChild.action = new Action("バーリア", "stub", 0, 0, effects);
                return createdChild;
            case "衝撃の符":
                createdChild.itemName = "衝撃の符";
                createdChild.description = "【衝撃の符】現在ターゲットしている敵に物理ダメージ。";
                createdChild.setImage("UI_Icon_Bomb");
                createdChild.setCount(9);
                createdChild.isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.TARGET_SINGLE, 30, Effect.EffectType.DAMAGE));
                createdChild.action = new Action("衝撃の符", "stub", 0, 0, effects);
                return createdChild;
            case "響火の符":
                createdChild.itemName = "響火の符";
                createdChild.description = "【響火の符】響火を発動。90Fの間、物理攻撃後に炎ダメージを追加。";
                createdChild.setImage("UI_Icon_Fire");
                createdChild.setCount(4);
                createdChild.isPassiveItem = false;
                createdChild.action = ActionStore.getActionByName("響火");
                return createdChild;
            case "消魔印":
                createdChild.itemName = "消魔印";
                createdChild.description = "【消魔印】ターンに1度、魔法ダメージを受けるときに自動発動。ダメージを1に軽減。";
                createdChild.setImage("UI_Icon_CardDiamonds");
                createdChild.setCount(3);
                createdChild.isPassiveItem = true;
                return createdChild;
            default:
                return createdChild;
        }
    }
}
