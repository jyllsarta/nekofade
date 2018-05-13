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
            case "ウィンドブーツ":
                description = "身軽になるブーツ。速+1,【先攻奪取】付与";
                enchants.Add(Buff.BuffID.CANSEL_ENEMYFIRSTTURN);
                return new Equip(0, 0, 0, 1, 0, 0, Equip.Rarity.EPIC, enchants, description);
            case "幸運の金貨":
                description = "持ち主に幸運を呼び込むといわれる金貨。 魔+1,収+1,防+1";
                return new Equip(0, 1, 1, 0, 1, 0, Equip.Rarity.RARE, enchants, description);
            case "デラックスソード":
                description = "デラックスソードはデラックスなソードだ！力+1,防+1";
                return new Equip(1, 0, 0, 0, 1, 0, Equip.Rarity.COMMON, enchants, description);
            case "闇":
                description = "周囲の物体を徐々に侵食し続ける闇の塊。全+1,【闇の侵食】付与";
                enchants.Add(Buff.BuffID.DARK_EROSION);
                return new Equip(1, 1, 1, 1, 1, 1, Equip.Rarity.LEGENDARY, enchants, description);
            default:
                description = "";
                return new Equip(0, 0, 0, 0, 0, 0, Equip.Rarity.COMMON, enchants, description);

        }
    }
}
