using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipStore{

    public static Equip getEquipByName(string name)
    {
        List<Buff.BuffID> enchants = new List<Buff.BuffID>();
        switch (name)
        {
            //Equipのコンストラクタは STR INT SPD DEF TOG 順
            case "プレートアーマー":
                return new Equip(0, 0, 0, 1, 1, enchants);
            case "しろこパフェ":
                return new Equip(0, 1, 0, 0, 1, enchants);
            case "ウィンドブーツ":
                return new Equip(0, 0, 2, 0, 0, enchants);
            case "幸運の金貨":
                return new Equip(1, 1, 0, 1, 0, enchants);
            case "デラックスソード":
                return new Equip(1, 0, 0, 1, 0, enchants);
            case "闇":
                enchants.Add(Buff.BuffID.DARK_EROSION);
                return new Equip(1, 1, 1, 1, 1, enchants);
            default:
                return new Equip(0, 0, 0, 0, 0, enchants);

        }
    }
}
