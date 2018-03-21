using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStore : MonoBehaviour {

    public Buff prefab;
    Buff createdChild;
    public MessageArea messageArea;
    public Buff instanciateBuffByBuffID(Buff.BuffID buffID, Transform parent)
    {
        createdChild = Instantiate(prefab, parent);
        createdChild.messageArea = messageArea;
        switch (buffID)
        {
            case Buff.BuffID.BLANK:
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
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.GUARD;
                createdChild.description = "防御している状態。次に受ける物理ダメージを大幅に軽減する。";
                createdChild.setImage("UI_Icon_Defend");
                createdChild.duplicates = true;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.POISON:
                createdChild.length = 300;
                createdChild.buffID = Buff.BuffID.POISON;
                createdChild.description = "毒状態。ターンの終了時に20点の固定ダメージ。";
                createdChild.setImage("UI_Icon_Skull");
                createdChild.duplicates = true;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.REGENERATE:
                createdChild.length = 180;
                createdChild.buffID = Buff.BuffID.REGENERATE;
                createdChild.description = "癒しの陣により再生力が増加している状態。1Fごとに1HP回復する。";
                createdChild.setImage("UI_Icon_HeartEmpty");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.DARK_EROSION:
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.DARK_EROSION;
                createdChild.description = "【闇の侵食】闇に飲まれている。ターンの終了時に25点の固定ダメージ。";
                createdChild.setImage("UI_Icon_FaithTaosim");
                createdChild.duplicates = true;
                createdChild.isPermanent = true;
                return createdChild;
            case Buff.BuffID.CANSEL_ENEMYFIRSTTURN:
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.CANSEL_ENEMYFIRSTTURN;
                createdChild.description = "【先攻奪取】初ターン一方的に行動できる。";
                createdChild.setImage("UI_Icon_Award");
                createdChild.duplicates = false;
                createdChild.isPermanent = true;
                return createdChild;
            case Buff.BuffID.POWERUP:
                createdChild.length = 300;
                createdChild.buffID = Buff.BuffID.POWERUP;
                createdChild.description = "【強化】筋力,魔力,収魔,防御,速度が1Lv上昇する。";
                createdChild.setImage("UI_Icon_Curves");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.STRUP:
                createdChild.length = 300;
                createdChild.buffID = Buff.BuffID.STRUP;
                createdChild.description = "【力溜】筋力LV+3。";
                createdChild.setImage("UI_Icon_Attack");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.INTUP:
                createdChild.length = 300;
                createdChild.buffID = Buff.BuffID.INTUP;
                createdChild.description = "【集中】魔力LV+3。";
                createdChild.setImage("UI_Icon_FaithJudaism");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.ENCHANT_FIRE:
                createdChild.length = 90;
                createdChild.buffID = Buff.BuffID.ENCHANT_FIRE;
                createdChild.description = "【響火】物理攻撃後にプチ火炎を発動。";
                createdChild.setImage("UI_Icon_Fire");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.DCS:
                createdChild.length = 200;
                createdChild.buffID = Buff.BuffID.DCS;
                createdChild.description = "【闇ポテトのスープ】筋力Lv+3,速度Lv+3。";
                createdChild.setImage("UI_Icon_GenderMale1");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.INVINCIBLE:
                createdChild.length = 60;
                createdChild.buffID = Buff.BuffID.INVINCIBLE;
                createdChild.description = "【無敵】敵からの攻撃によるダメージを受けない。";
                createdChild.setImage("UI_Icon_Star");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            default:
                Debug.LogWarning("getBuffByBuffIDのデフォルトが呼ばれてる");
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.BLANK;
                createdChild.description = "よりひどい空のバフ。ゲーム中だと見えないはずなんだけどなー...";
                createdChild.setImage("UI_Icon_Warning");
                createdChild.duplicates = false;
                return createdChild;
        }
    }
}
