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
        createdChild.buffID = buffID;
        switch (buffID)
        {
            case Buff.BuffID.BLANK:
                createdChild.length = 999;
                createdChild.description = "空のバフ。ゲーム中だと見えないはずなんだけどなー...";
                createdChild.setImage("warning");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
                //到達できないコードパスの緑線出るけどswitchでbreak書かないの嫌すぎ
            case Buff.BuffID.GUARD:
                Debug.LogWarning("GUARDは呼ばれないはず");
                createdChild.length = 999;
                createdChild.description = "防御している状態。次に受ける物理ダメージを大幅に軽減する。";
                createdChild.setImage("shield");
                createdChild.duplicates = true;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.POISON:
                createdChild.length = 100;
                createdChild.description = "毒状態。ターンの終了時に20点の固定ダメージ。";
                createdChild.setImage("poison");
                createdChild.duplicates = true;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.REGENERATE:
                createdChild.length = 100;
                createdChild.description = "癒しの陣により再生力が増加している状態。1Fごとに1HP回復する。";
                createdChild.setImage("regenerate");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.DARK_EROSION:
                createdChild.length = 999;
                createdChild.description = "【闇の侵食】闇に飲まれている。ターンの終了時に20点の固定ダメージ。";
                createdChild.setImage("erosion");
                createdChild.duplicates = true;
                createdChild.isPermanent = true;
                return createdChild;
            case Buff.BuffID.CANSEL_ENEMYFIRSTTURN:
                createdChild.length = 999;
                createdChild.description = "【先攻奪取】初ターン一方的に行動できる。";
                createdChild.setImage("sippu");
                createdChild.duplicates = false;
                createdChild.isPermanent = true;
                return createdChild;
            case Buff.BuffID.POWERUP:
                createdChild.length = 150;
                createdChild.description = "【強化】筋力,魔力,収魔,防御,速度が1Lv上昇する。";
                createdChild.setImage("powerup");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.STRUP:
                createdChild.length = 150;
                createdChild.description = "【力溜】筋力LV+3。";
                createdChild.setImage("strup");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.INTUP:
                createdChild.length = 150;
                createdChild.description = "【集中】魔力LV+3。";
                createdChild.setImage("magicup");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.ENCHANT_FIRE:
                createdChild.length = 90;
                createdChild.description = "【響火】物理攻撃後にランダム対象にプチ火炎を発動。";
                createdChild.setImage("hibihi");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.DCS:
                createdChild.length = 100;
                createdChild.description = "【闇ポテトのスープ】筋力Lv+3,速度Lv+3。";
                createdChild.setImage("DCS");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.INVINCIBLE:
                createdChild.length = 30;
                createdChild.description = "【無敵】敵からの攻撃によるダメージを受けない。";
                createdChild.setImage("invincible");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.SPDUP:
                createdChild.length = 100;
                createdChild.description = "【加速】速度Lv+3。";
                createdChild.setImage("sippu");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.ADDITIONAL_ATTACK:
                createdChild.length = 90;
                createdChild.description = "【幻閃】ターン開始時に今ターゲットしている敵に追加の攻撃。";
                createdChild.setImage("gensen");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.ADDITIONAL_ENDATTACK:
                createdChild.length = 90;
                createdChild.description = "【追幻】ターン終了時にランダムな敵に強力な追加の攻撃。";
                createdChild.setImage("tuigen");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.ADDITIONAL_MAGIC:
                createdChild.length = 90;
                createdChild.description = "【鏡射】ターン終了時に今ターゲットしている敵に強力な追加の攻撃。";
                createdChild.setImage("kyousha");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.AUTO_SHIELD:
                createdChild.length = 120;
                createdChild.description = "【残像】ターン開始時に1枚防壁を追加。";
                createdChild.setImage("addShield");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.REGENERATE_MP:
                createdChild.length = 120;
                createdChild.description = "【霊祈】ターン終了時にMPを回復。残りMPが少ないほど効果大。";
                createdChild.setImage("reisetu");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            case Buff.BuffID.EXALTED:
                createdChild.length = 50;
                createdChild.description = "【高揚】与えるダメージが2倍。";
                createdChild.setImage("exalted");
                createdChild.duplicates = false;
                createdChild.isPermanent = false;
                return createdChild;
            default:
                Debug.LogWarning("getBuffByBuffIDのデフォルトが呼ばれてる");
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.BLANK;
                createdChild.description = "よりひどい空のバフ。ゲーム中だと見えないはずなんだけどなー...";
                createdChild.setImage("warning");
                createdChild.duplicates = false;
                return createdChild;
        }
    }
}
