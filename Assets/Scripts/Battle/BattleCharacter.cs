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
    public int intelligence;
    public int magicCapacity;
    public int speed;
    public int defence;
    public int vitality;

    //思考ルーチン系
    public int currentActionPosition;
    public RoutineType routine;

    //倒した時落とす金額
    public int rewardGold;

    //今かかってる効果一覧
    public List<Buff> buffs;

    //技一覧
    public List<string> actions;
    //キャラのアトリビュート
    public List<CharacterAttribute.AttributeID> attributes;

    //自身がクリックされたことを伝えるためにバトルシーンへの参照を握っておく
    public Battle battle;

    public NumeratableSlider hpGauge;
    public NumeratableSlider mpGauge;
    public NumeratableText hpText;
    public NumeratableText mpText;
    public TextMeshProUGUI characterNameText;
    public GameObject buffContainer;

    public ShieldsContainer shieldContainer;

    public Animator animator;

    //自身の画像
    public Image image;

    //プレイヤー用 表情コントローラ
    public CharacterEmotionController emote;

    //思考ルーチン
    public enum RoutineType
    {
        ASCENDING, //リスト順
        ASCENDING_RANDOMSTART, //初期位置ランダムだけどリスト順
        RANDOM, //ランダム
    }

    public bool isDead()
    {
        return hp <= 0;
    }

    public void setEmotion(string emotionName,int length=-1)
    {
        if (!emote)
        {
            return;
        }
        emote.setEmotion(emotionName,length);
    }

    void Start()
    {
        buffs = new List<Buff>();
        //attributes = new List<CharacterAttribute.AttributeID>();
        hpGauge.setMaxValue(maxHp);
        hpGauge.set(maxHp);
        shieldCount = 0;
        currentActionPosition = 0;
        if (routine == RoutineType.ASCENDING_RANDOMSTART)
        {
            currentActionPosition = UnityEngine.Random.Range(0, actions.Count);
        }
        shieldContainer.initialize(getMaxDefenceCount(), shieldCount);
    }

    //*************************************
    //パラメータからステータス実数値を算出
    //*************************************
    //攻撃倍率
    public float getAttackRate()
    {
        int str = strength;
        if (hasBuff(Buff.BuffID.POWERUP))
        {
            str++;
        }
        if (hasBuff(Buff.BuffID.STRUP))
        {
            str += 3;
        }
        if (hasBuff(Buff.BuffID.DCS))
        {
            str += 3;
        }
        return 1 + str * 0.4f;
    }
    //魔法攻撃倍率
    public float getMagicRate()
    {
        int inte = intelligence;
        if (hasBuff(Buff.BuffID.POWERUP))
        {
            inte++;
        }
        if (hasBuff(Buff.BuffID.INTUP))
        {
            inte += 3;
        }
        return 1 + inte * 0.4f;
    }
    //最大HP
    public int getMaxHP()
    {
        return 100 + vitality * 40;
    }
    //最大HP
    public int getMaxMP()
    {
        return 100 + magicCapacity * 40;
    }
    //MP自然回復率 収魔Lv3以上なら10、それ以外なら5点回復
    public int getMPHealRate()
    {
        int mc = magicCapacity;
        if (hasBuff(Buff.BuffID.POWERUP))
        {
            mc++;
        }
        return mc>=3 ? 10 : 5;
    }
    //防御カット率
    public float getDefenceCutRate()
    {
        int def = defence;
        if (hasBuff(Buff.BuffID.POWERUP))
        {
            def++;
        }
        if (def > 9)
        {
            def = 9;
        }
        return getDefaultWaitTimeCutRate(def);
    }

    public static float getDefaultDefenceCutRate(int defenceLevel)
    {
        switch (defenceLevel)
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
            case 8:
                return 0.91f;
            case 9:
                return 0.92f;
            default:
                Debug.LogWarning("getDefenceCutRateのdefault呼ばれた");
                return 1f;
        }
    }

    //恒常防御カット率
    public float getNormalCutRate()
    {
        int def = defence;
        if (hasBuff(Buff.BuffID.POWERUP))
        {
            def++;
        }
        if (def > 9)
        {
            def = 9;
        }
        return getDefaultNormalCutRate(def);
    }
    public static float getDefaultNormalCutRate(int defenceLevel)
    {
        switch (defenceLevel)
        {
            case 0:
                return 0.0f;
            case 1:
                return 0.08f;
            case 2:
                return 0.16f;
            case 3:
                return 0.24f;
            case 4:
                return 0.32f;
            case 5:
                return 0.40f;
            case 6:
                return 0.48f;
            case 7:
                return 0.56f;
            case 8:
                return 0.64f;
            case 9:
                return 0.72f;
            default:
                Debug.LogWarning("getDefenceCutRateのdefault呼ばれた");
                return 1f;
        }
    }

    public static float getDefaultWaitTimeCutRate(int speedLevel)
    {
        switch (speedLevel)
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
            case 8:
                return 0.48f;
            case 9:
                return 0.46f;
            default:
                Debug.LogWarning("getWaitTimeCutRateのdefault呼ばれた");
                return 1f;
        }
    }

    //ウェイトカット率
    public float getWaitTimeCutRate()
    {
        int spd = speed;
        if (hasBuff(Buff.BuffID.POWERUP))
        {
            spd++;
        }
        if (hasBuff(Buff.BuffID.DCS))
        {
            spd += 3;
        }
        if (hasBuff(Buff.BuffID.SPDUP))
        {
            spd += 3;
        }
        if (spd > 9)
        {
            spd = 9;
        }
        return getDefaultWaitTimeCutRate(spd);
    }
    //最大防御回数 Lv3, Lv6で追加+1回
    //0123456789
    //1112223334
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
            //非恒久バフのみ処理
            if (!buff.isPermanent)
            {
                //残存期間を減らす
                buff.length -= 1;

                //見た目を更新
                buff.text.text = buff.length.ToString();
            }
        }
        //removeAllのためにtrueを返しながら自身を画面上から消す副作用のあるラムダ式
        Func<Buff, bool> destroyIt = (x) => { Destroy(x.gameObject); return true; };
        //残存期間0のものをDestroyしながら自身のもつ参照からも消してく
        buffs.RemoveAll(x => (x.length <= 0 ? destroyIt(x) : false));
    }

    public bool hasBuff(Buff.BuffID buffID)
    {
        return buffs.FindIndex(b => b.buffID == buffID) != -1;
    }

    public int getBuffCount(Buff.BuffID buffID)
    {
        int count = 0;
        foreach (Buff b in buffs) {
            if (b.buffID == buffID)
            {
                count++;
            }
        }
        return count;
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
        int poison_count = getBuffCount(Buff.BuffID.POISON);
        if (poison_count > 0)
        {
            //Debug.Log(string.Format("{0}は毒で{1}ダメージ!", characterName, 20 * poison_count));
            setEmotion("damage",90);
            hp -= 10 * poison_count;
            battle.playDamageEffect(10 * poison_count, this);
        }
        //闇侵食
        int erosion_count = getBuffCount(Buff.BuffID.DARK_EROSION);
        if (erosion_count > 0)
        {
            Debug.Log(string.Format("{0}は闇の侵食で{1}ダメージ!", characterName, 25 * erosion_count));
            setEmotion("damage", 90);
            hp -= 20 * erosion_count;
            battle.playDamageEffect(20 * erosion_count, this);
        }
        //残像
        int shield_count = getBuffCount(Buff.BuffID.AUTO_SHIELD);
        if (shield_count > 0)
        {
            for (int i=0;i<shield_count;++i)
            {
                addShield();
            }
        }
        if (hasBuff(Buff.BuffID.REGENERATE_MP) && mp < maxMp)
        {
            float mp_percentage = mp / maxMp;
            float recovery_rate = 0.1f;
            int constant_healpoint = 5;
            mp += (int)( constant_healpoint + (1 - mp_percentage)*recovery_rate * maxMp);
            if (mp > maxMp)
            {
                mp = maxMp;
            }
        }
    }

    //このキャラはこのアトリビュートを持ってるかな?
    public bool hasAttribute(CharacterAttribute.AttributeID attr)
    {
        return attributes.Contains(attr);
    }

    //アトリビュートを追加
    public void addAttribute(CharacterAttribute.AttributeID attr)
    {
        attributes.Add(attr);
    }

    public void setActionPositionToNext()
    {
        currentActionPosition++;
        if (currentActionPosition >= actions.Count)
        {
            currentActionPosition = 0;
        }
    }

    //アクションを取り、次のアクションへ
    public string nextAction()
    {
        string actionName;
        switch (routine)
        {
            case RoutineType.ASCENDING:
                actionName = actions[currentActionPosition];
                break;
            case RoutineType.ASCENDING_RANDOMSTART:
                actionName = actions[currentActionPosition];
                break;
            case RoutineType.RANDOM:
                actionName = actions[UnityEngine.Random.Range(0,actions.Count)];
                break;
            default:
                Debug.LogWarning("nextActionでdefaultラベル飛んでる");
                actionName = "";
                break;
        }
        setActionPositionToNext();
        return actionName;
    }

    //こいつがここにあるのが正しいかどうかわからないけどとりあえず置いておく
    //1ターンに一度行う処理
    public void OnTurnStart()
    {
        shieldContainer.initialize(getMaxDefenceCount(), shieldCount);
    }

    //こいつがここにあるのが正しいかどうかわからないけどとりあえず置いておく
    //1ターンに一度行う処理
    public void OnTurnEnd()
    {
        activateTurnBuffEffect();
        //MP回復処理 エフェクトが付いたら関数に隔離
        mp += getMPHealRate();
        if (mp > maxMp)
        {
            mp = maxMp;
        }
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
            hpGauge.toValue = hp;
            hpGauge.setMaxValue(maxHp);
        }
        if (hpText)
        {
            hpText.numerate(hp);
        }
        if (mpGauge)
        {
            mpGauge.toValue = mp;
            mpGauge.setMaxValue(maxMp);
        }
        if (mpText)
        {
            mpText.numerate(mp);
        }
    }

    public void setImage(string resourcePath)
    {
        Sprite s = Resources.Load<Sprite>(resourcePath);
        image.sprite = s;
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
            hp += 3;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
        //防御枚数の更新　重ければ厳密に加減のタイミングで取るようにする
        shieldContainer.updateShieldCount(shieldCount);
    }

    //ほにゃほにゃ～っと死ぬ感じのアニメーションして死ぬ
    public void playDeathAnimationThenRemoveFromScene()
    {
        animator.Play("Death");
    }

    //ダメージ食らうアニメーションする
    public void playDamageAnimation()
    {

        animator.Play("hit");
    }

    //パラメータを一気に設定 vitは死にパラになるけどまあしかたなし
    public void initializeParameters(int hp, int mp, int strength, int intelligence, int speed, int defence, int vitality)
    {
        this.hp = hp;
        this.maxHp = hp;
        this.mp = mp;
        this.maxMp = mp;
        this.strength = strength;
        this.intelligence = intelligence;
        this.speed = speed;
        this.defence = defence;
        this.vitality = vitality;
    }

    //毎フレーム行う処理 
    void Update()
    {
        updateView();
    }

}
