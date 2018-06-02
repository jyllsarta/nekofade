using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipStore{

    public static Equip getEquipByName(string name)
    {
        List<Buff.BuffID> enchants = new List<Buff.BuffID>();
        string description;
        switch (name)
        {
            //Equipのコンストラクタは STR INT SPD DEF TOG 順
            case "プレートアーマー":
                description = "硬い金属板を丁寧に張り合わせて作った鎧。防+1,体+1";
                return new Equip(0, 0, 0, 0, 1, 1, Equip.Rarity.COMMON, enchants, description);
            case "しろこパフェ":
                description = "しろこ特製のパフェ。あなたそんな趣味ありましたっけ...? 魔+1,体+1";
                return new Equip(0, 1, 0, 0, 0, 1, Equip.Rarity.COMMON, enchants, description);
            case "さんかく帽子":
                description = "魔法使い用の帽子。収+2, 魔+1(最大MP+80, 魔ダメ+40%)";
                return new Equip(0, 1, 2, 0, 0, 0, Equip.Rarity.COMMON, enchants, description);
            case "ウィンドブーツ":
                description = "身軽になるブーツ。速+1,【先攻奪取】付与";
                enchants.Add(Buff.BuffID.CANSEL_ENEMYFIRSTTURN);
                return new Equip(0, 0, 0, 1, 0, 0, Equip.Rarity.EPIC, enchants, description);
            case "霊気の鎧":
                description = "霊気をまとった鎧。体+2, 初ターン無敵。";
                enchants.Add(Buff.BuffID.INVINCIBLE);
                return new Equip(0, 0, 0, 0, 0, 2, Equip.Rarity.RARE, enchants, description);
            case "幸運の金貨":
                description = "持ち主に幸運を呼び込むといわれる金貨。 魔+1,収+1,防+1";
                return new Equip(0, 1, 1, 0, 1, 0, Equip.Rarity.RARE, enchants, description);
            case "デラックスソード":
                description = "デラックスソードはデラックスなソードだ！力+1,防+1";
                return new Equip(1, 0, 0, 0, 1, 0, Equip.Rarity.COMMON, enchants, description);
            case "闇":
                description = "侵食を続ける闇の塊。全ステ+1, 毎ターン固定20ダメージ。";
                enchants.Add(Buff.BuffID.DARK_EROSION);
                return new Equip(1, 1, 1, 1, 1, 1, Equip.Rarity.EPIC, enchants, description);
            default:
                description = "";
                return new Equip(0, 0, 0, 0, 0, 0, Equip.Rarity.COMMON, enchants, description);

        }
    }
}
