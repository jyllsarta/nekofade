using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStore : MonoBehaviour{

    public static Item getItemByName(string name)
    {

        string itemName;
        string description;
        string imagePath;
        int count;
        bool isPassiveItem;
        List<Effect> effects = new List<Effect>();
        Action action;

        switch (name)
        {
            case "復活の御魂": //こいつはだけパッシブ発動で特別処理書くのでいいかもな
                itemName = "復活の御魂";
                description = "【復活の御魂】HPが0になるダメージを受けたときに自動発動。HP全快で復活する。";
                imagePath = "UI_Icon_Infinite";
                count = 2;
                isPassiveItem = true;
                action = new Action();
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "HPポーション":
                itemName = "HPポーション";
                description = "【HPポーション】HPを50回復する。HP上限を超えて回復できる。";
                imagePath = "UI_Icon_Trophy";
                count = 5;
                isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 50, Effect.EffectType.CONSTANTEXCEEDHEAL));
                action = new Action("HPポーション", "stub", 0, 0, effects);
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "MPポーション":
                itemName = "MPポーション";
                description = "【MPポーション】MPを50回復する。MP上限を超えて回復できる。";
                imagePath = "UI_Icon_FaithIslam";
                count = 5;
                isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 50, Effect.EffectType.CONSTANTEXCEEDMPHEAL));
                action = new Action("MPポーション", "stub", 0, 0, effects);
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "DCS":
                itemName = "魔ポテトのスープ";
                description = "【魔ポテトのスープ】HPを40回復し、200Fの間スープ状態になる。筋力Lv+3,速度Lv+3。";
                imagePath = "UI_Icon_FaithIslam";
                count = 1;
                isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 40, Effect.EffectType.CONSTANTHEAL));
                effects.Add(new Effect(Effect.TargetType.ME, 99, Buff.BuffID.DCS));
                action = new Action("魔ポテ", "stub", 0, 0, effects);
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "雷光の符":
                itemName = "雷光の符";
                description = "【雷光の符】雷光を発動。ランダムな敵に合計4回魔法ダメージ。";
                imagePath = "UI_Icon_Energy";
                count = 4;
                isPassiveItem = false;
                action = ActionStore.getActionByName("雷光");
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "無敵バリア":
                itemName = "無敵バリア";
                description = "【無敵バリア】60Fの間、敵からのダメージをすべて無効化するバリアを展開。";
                imagePath = "UI_Icon_Star";
                count = 2;
                isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.ME, 99, Buff.BuffID.INVINCIBLE));
                action = new Action("バーリア", "stub", 0, 0, effects);
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "衝撃の符":
                itemName = "衝撃の符";
                description = "【衝撃の符】現在ターゲットしている敵に物理ダメージ。";
                imagePath = "UI_Icon_Bomb";
                count = 9;
                isPassiveItem = false;
                effects.Add(new Effect(Effect.TargetType.TARGET_SINGLE, 30, Effect.EffectType.DAMAGE));
                action = new Action("衝撃の符", "stub", 0, 0, effects);
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "響火の符":
                itemName = "響火の符";
                description = "【響火の符】響火を発動。90Fの間、物理攻撃後に炎ダメージを追加。";
                imagePath = "UI_Icon_Fire";
                count = 4;
                isPassiveItem = false;
                action = ActionStore.getActionByName("響火");
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            case "消魔印":
                itemName = "消魔印";
                description = "【消魔印】ターンに1度、魔法ダメージを受けるときに自動発動。ダメージを1に軽減。";
                imagePath = "UI_Icon_CardDiamonds";
                count = 3;
                isPassiveItem = true;
                action = new Action();
                return new Item(itemName,count,isPassiveItem,description,imagePath,action);
            default:
                return new Item();
        }
    }
}
