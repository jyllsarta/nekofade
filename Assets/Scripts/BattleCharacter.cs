using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;


//敵味方共通の処理
public class BattleCharacter : MonoBehaviour {

    public string characterName;
    //戦闘中のパラメータ
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;
    //今積まれている防御盾の枚数
    public int shieldCount;

    //各種基礎パラメータ
    public int strength;
    public int defence;
    public int intelligence;
    public int speed;
    public int toughness;

    //今かかってる効果一覧
    public List<Buff> buffs;

    //技一覧
    public List<string> actions;
    //キャラのアトリビュート
    public List<CharacterAttribute.AttributeID> attributes;

    //自身がクリックされたことを伝えるためにバトルシーンへの参照を握っておく
    public Battle battle;

    public Slider hpGauge;
    public Slider mpGauge;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public GameObject buffContainer;

    public bool isDead()
    {
        return hp <= 0;
    }

    void Start()
    {
        buffs = new List<Buff>();
        attributes = new List<CharacterAttribute.AttributeID>();
        hpGauge.maxValue = maxHp;
        hpGauge.value = maxHp;
        shieldCount = 0;
    }

    //*************************************
    //パラメータからステータス実数値を算出
    //*************************************
    //攻撃倍率
    public float getAttackRate()
    {
        return 1 + strength * 0.4f;
    }
    //魔法攻撃倍率
    public float getMagicRate()
    {
        return 1 + intelligence * 0.4f;
    }
    //最大HP
    public int getMaxHP()
    {
        return 100 + toughness * 40;
    }
    //防御カット率
    public float getDefenceCutRate()
    {
        switch (defence)
        {
            case 0:
                return 0.5f;
            case 1:
                return 0.66f;
            case 2:
                return 0.75f;
            case 3:
                return 0.80f;
            case 4:
                return 0.83f;
            case 5:
                return 0.86f;
            case 6:
                return 0.88f;
            case 7:
                return 0.90f;
            default:
                Debug.LogWarning("getDefenceCutRateのdefault呼ばれた");
                return 1f;
        }
    }
    //防御カット率
    public float getWaitTimeCutRate()
    {
        switch (speed)
        {
            case 0:
                return 1f;
            case 1:
                return 0.90f;
            case 2:
                return 0.81f;
            case 3:
                return 0.73f;
            case 4:
                return 0.66f;
            case 5:
                return 0.60f;
            case 6:
                return 0.55f;
            case 7:
                return 0.51f;
            default:
                Debug.LogWarning("getWaitTimeCutRateのdefault呼ばれた");
                return 1f;
        }
    }
    //最大防御回数 Lv3, Lv6で追加+1回
    //01234567
    //11122233
    public int getMaxDefenceCount()
    {
        return 1 + defence / 3;
    }

    //防御障壁を1追加する
    public void addShield()
    {
        if (shieldCount >= getMaxDefenceCount())
        {
            Debug.LogFormat("{0}は防御障壁をもう張れない！",characterName);
            return;
        }
        shieldCount++;
    }

    //防御を持ってるか
    public bool hasShield()
    {
        return shieldCount > 0;
    }

    //バフの残存期間を1F減らし、0になったものを削除する
    public void updateBuffState()
    {
        foreach(Buff buff in buffs)
        {
            //残存期間を減らす
            buff.length -= 1;

            //見た目を更新
            buff.text.text = buff.length.ToString();
        }
        //removeAllのためにtrueを返しながら自身を画面上から消す副作用のあるラムダ式
        Func<Buff, bool> destroyIt = (x) => { Destroy(x.gameObject); return true; };
        //残存期間0のものをDestroyしながら自身のもつ参照からも消してく
        buffs.RemoveAll(x => (x.length < 0 ? destroyIt(x) : false));
    }

    public bool hasBuff(Buff.BuffID buffID)
    {
        return buffs.FindIndex(b => b.buffID == buffID) != -1;
    }

    //指定したバフIDのバフを全部消す
    public void removeBuff(Buff.BuffID buffID)
    {
        //removeAllのためにtrueを返しながら自身を画面上から消す副作用のあるラムダ式
        Func<Buff, bool> destroyIt = (x) => { Destroy(x.gameObject); return true; };
        //残存期間0のものをDestroyしながら自身のもつ参照からも消してく
        buffs.RemoveAll(x => (x.buffID==buffID ? destroyIt(x) : false));
    }

    //ターン毎のバフ効果の処理を行う
    void activateTurnBuffEffect()
    {
        //毒状態なら10点のダメージを受ける
        if (hasBuff(Buff.BuffID.POISON))
        {
            Debug.Log(string.Format("{0}は毒で10ダメージ!", characterName));
            hp -= 10;
        }
    }

    //このキャラはこのアトリビュートを持ってるかな?
    public bool hasAttribute(CharacterAttribute.AttributeID attr)
    {
        return attributes.Contains(attr);
    }

    //こいつがここにあるのが正しいかどうかわからないけどとりあえず置いておく
    //1ターンに一度行う処理
    public void OnTurnEnd()
    {
        activateTurnBuffEffect();
    }

    //aのコスト払えるかチェック
    public bool canPayThis(Action a)
    {
        return mp >= a.cost;　//TODO MP消費カット装備やらアビリティの反映
    }

    //actionsのコスト合計を払えるかチェック
    public bool canPayThis(List<Action> actions)
    {
        int sum = 0;
        foreach (Action a in actions)
        {
            sum += a.cost;
        }
        return mp >= sum;　//TODO MP消費カット装備やらアビリティの反映
    }

    //aのコストを支払う
    public void payCastCost(Action a)
    {
        mp -= a.cost; //TODO MP消費カット装備やらアビリティの反映
    }

    //aのコストを払い戻す
    public void returnCastCost(Action a)
    {
        mp += a.cost; //TODO MP消費カット装備やらアビリティの反映
    }

    void updateView()
    {
        //各種ゲージがない敵もいる
        if (hpGauge)
        {
            hpGauge.value = hp;
            hpGauge.maxValue = maxHp;
        }
        if (hpText)
        {
            hpText.text = hp.ToString();

        }
        if (mpGauge)
        {
            mpGauge.value = mp;
        }
        if (mpText)
        {
            mpText.text = mp.ToString();

        }
    }

    //クリック時
    public void OnClick()
    {
        battle.targetEnemyByHash(this.GetHashCode());
    }

    //ゲーム内フレームごとに行う処理
    public void onEveryFrame()
    {
        if (hasBuff(Buff.BuffID.REGENERATE) && hp < maxHp)
        {
            hp += 1;
        }
    }

    //毎フレーム行う処理 
    void Update()
    {
        updateView();
    }

}
