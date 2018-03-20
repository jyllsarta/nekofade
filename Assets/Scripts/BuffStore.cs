using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStore : MonoBehaviour {

    public Buff prefab;
    Buff createdChild;
    public Buff instanciateBuffByBuffID(Buff.BuffID buffID, Transform parent)
    {
        switch (buffID)
        {
            case Buff.BuffID.BLANK:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.BLANK;
                createdChild.description = "空のバフ。ゲーム中だと見えないはずなんだけどなー...";
                createdChild.setImage("UI_Icon_Warning");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
                //到達できないコードパスの緑線出るけどswitchでbreak書かないの嫌すぎ
            case Buff.BuffID.GUARD:
                Debug.LogWarning("GUARDは呼ばれないはず");
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.GUARD;
                createdChild.description = "防御している状態。次に受ける物理ダメージを大幅に軽減する。";
                createdChild.setImage("UI_Icon_Defend");
                createdChild.duplicates = true;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.POISON:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 300;
                createdChild.buffID = Buff.BuffID.POISON;
                createdChild.description = "毒状態。ターンの終了時に20点の固定ダメージ。";
                createdChild.setImage("UI_Icon_Skull");
                createdChild.duplicates = true;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.REGENERATE:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 180;
                createdChild.buffID = Buff.BuffID.REGENERATE;
                createdChild.description = "癒しの陣により再生力が増加している状態。1Fごとに1HP回復する。";
                createdChild.setImage("UI_Icon_HeartEmpty");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.DARK_EROSION:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.DARK_EROSION;
                createdChild.description = "【闇の侵食】闇に飲まれている。ターンの終了時に25点の固定ダメージ。";
                createdChild.setImage("UI_Icon_FaithTaosim");
                createdChild.duplicates = true;
                createdChild.isPermanent = true;
                return createdChild;
            case Buff.BuffID.CANSEL_ENEMYFIRSTTURN:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.CANSEL_ENEMYFIRSTTURN;
                createdChild.description = "【先攻奪取】初ターン一方的に行動できる。";
                createdChild.setImage("UI_Icon_Award");
                createdChild.duplicates = false;
                createdChild.isPermanent = true;
                return createdChild;
            case Buff.BuffID.POWERUP:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 300;
                createdChild.buffID = Buff.BuffID.POWERUP;
                createdChild.description = "【強化】筋力,魔力,収魔,防御,速度が1Lv上昇する。";
                createdChild.setImage("UI_Icon_Curves");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            default:
                Debug.LogWarning("getBuffByBuffIDのデフォルトが呼ばれてる");
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.BLANK;
                createdChild.description = "よりひどい空のバフ。ゲーム中だと見えないはずなんだけどなー...";
                createdChild.setImage("UI_Icon_Warning");
                createdChild.duplicates = false;
                return createdChild;
        }
    }
}
