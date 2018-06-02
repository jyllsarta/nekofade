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
        Item.Rarity rarity;
        Action action;

        switch (name)
        {
            case "復活の御魂": //こいつはだけパッシブ発動で特別処理書くのでいいかもな
                itemName = "復活の御魂";
                description = "【復活の御魂】HPが0になるダメージを受けたときに自動発動。HP全快で復活する。";
                imagePath = "mitama";
                count = 1;
                isPassiveItem = true;
                rarity = Item.Rarity.LEGENDARY;
                action = new Action();
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity); ;
            case "HPポーション":
                itemName = "HPポーション";
                description = "【HPポーション】HPを300回復する。HP上限を超えて回復できる。";
                imagePath = "HPpotion";
                count = 1;
                isPassiveItem = false;
                rarity = Item.Rarity.COMMON;
                effects.Add(new Effect(itemName, Effect.TargetType.ME, 300, Effect.EffectType.CONSTANTEXCEEDHEAL));
                action = new Action("HPポーション", "stub", 0, 0, effects);
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            case "MPポーション":
                itemName = "MPポーション";
                description = "【MPポーション】MPを150回復する。MP上限を超えて回復できる。";
                imagePath = "MPpotion";
                count = 1;
                isPassiveItem = false;
                rarity = Item.Rarity.COMMON;
                effects.Add(new Effect(itemName, Effect.TargetType.ME, 150, Effect.EffectType.CONSTANTEXCEEDMPHEAL));
                action = new Action("MPポーション", "stub", 0, 0, effects);
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            case "DCS":
                itemName = "魔ポテトのスープ";
                description = "【魔ポテトのスープ】HPを40回復し、100Fの間スープ状態になる。筋力Lv+3,速度Lv+3。";
                imagePath = "DCS";
                count = 1;
                isPassiveItem = false;
                rarity = Item.Rarity.EPIC;
                effects.Add(new Effect(itemName, Effect.TargetType.ME, 40, Effect.EffectType.CONSTANTHEAL));
                effects.Add(new Effect(itemName, Effect.TargetType.ME, 99, Buff.BuffID.DCS));
                action = new Action("魔ポテ", "stub", 0, 0, effects);
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            case "雷光の符":
                itemName = "雷光の符";
                description = "【雷光の符】雷光を発動。ランダムな敵に合計4回魔法ダメージ。";
                imagePath = "cardThunder";
                count = 1;
                isPassiveItem = false;
                rarity = Item.Rarity.EPIC;
                action = ActionStore.getActionByName("雷光");
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            case "無敵バリア":
                itemName = "無敵バリア";
                description = "【無敵バリア】30Fの間、敵からのダメージをすべて無効化するバリアを展開。";
                imagePath = "invincible";
                count = 1;
                isPassiveItem = false;
                rarity = Item.Rarity.LEGENDARY;
                effects.Add(new Effect(itemName, Effect.TargetType.ME, 99, Buff.BuffID.INVINCIBLE));
                action = new Action("バーリア", "stub", 0, 0, effects);
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            case "衝撃の符":
                itemName = "衝撃の符";
                description = "【衝撃の符】現在ターゲットしている敵に物理ダメージ。";
                imagePath = "cardShock";
                count = 1;
                isPassiveItem = false;
                rarity = Item.Rarity.COMMON;
                effects.Add(new Effect(itemName, Effect.TargetType.TARGET_SINGLE, 30, Effect.EffectType.DAMAGE));
                action = new Action("衝撃の符", "stub", 0, 0, effects);
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            case "響火の符":
                itemName = "響火の符";
                description = "【響火の符】響火を発動。45Fの間、物理攻撃後に炎ダメージを追加。";
                imagePath = "cardEnchantFire";
                count = 1;
                isPassiveItem = false;
                rarity = Item.Rarity.RARE;
                action = ActionStore.getActionByName("響火");
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            case "消魔印":
                itemName = "消魔印";
                description = "【消魔印】魔法ダメージを受けるときに自動発動。ダメージを1に軽減。";
                imagePath = "nullifyMagic";
                count = 1;
                isPassiveItem = true;
                rarity = Item.Rarity.COMMON;
                action = new Action();
                return new Item(itemName, count, isPassiveItem, description, imagePath, action, rarity);
            default:
                return new Item();
        }
    }
}
