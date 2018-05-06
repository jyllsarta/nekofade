using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStore : MonoBehaviour{


    //actorを指定すると素早さ補正を反映したアクションを返す
    public static Action getActionByName(string actionName="", BattleCharacter actor=null)
    {
        // new Effect文が長くなるのでここで転記しておく(ダサいけど多分読みやすくなるから許して)
        Effect.TargetType TARGET_SINGLE = Effect.TargetType.TARGET_SINGLE;
        Effect.TargetType TARGET_SINGLE_RANDOM = Effect.TargetType.TARGET_SINGLE_RANDOM;
        Effect.TargetType TARGET_ALL = Effect.TargetType.TARGET_ALL;
        Effect.TargetType ME = Effect.TargetType.ME;
        //Effect.TargetType ALLY_SINGLE_RANDOM = Effect.TargetType.ALLY_SINGLE_RANDOM;
        Effect.TargetType ALLY_ALL = Effect.TargetType.ALLY_ALL;

        Effect.EffectType DAMAGE = Effect.EffectType.DAMAGE;
        Effect.EffectType HEAL = Effect.EffectType.HEAL;
        //Effect.EffectType BUFF = Effect.EffectType.BUFF;

        string description;
        int waitTime;
        int power=1;
        int cost;
        List<Effect> effects = new List<Effect>();
        List<Effect.Attribute> attributes = new List<Effect.Attribute>();

        switch (actionName)
        {
            case "":
                Debug.LogWarning("getActionByNameの空文字コンストラクタが呼ばれてる");
                return new Action();
            case "攻撃":
                description = "通常の攻撃を行う。";
                waitTime = 12;
                power = 35;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "刺突":
                description = "すばやく敵を一突き。威力は大きくないが、隙が少ない。";
                waitTime = 10;
                power = 23;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "重撃":
                description = "大振りの一撃を加える。防御していない敵に対して非常に有効。";
                waitTime = 18;
                power = 131;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "双撃":
                description = "すばやく2連撃を行う。防御を崩したり詠唱妨害が得意。";
                waitTime = 13;
                power = 31;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 40));
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 40));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "盾打":
                description = "障壁を1枚展開しつつ攻撃する。";
                waitTime = 13;
                power = 56;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                effects.Add(new Effect(ME, power, Buff.BuffID.GUARD));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "王衝":
                description = "3ターン目以降に威力が3倍となる一撃。そろそろ本気出す！";
                waitTime = 12;
                power = 30;
                cost = 0;
                attributes.Add(Effect.Attribute.PETIT_KING);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "真王":
                description = "5ターン目以降に威力が7倍となる一撃。すべてを終わらせる時が来たようだな";
                waitTime = 20;
                power = 52;
                cost = 0;
                attributes.Add(Effect.Attribute.KING);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "王舞":
                description = "5ターン目以降威力7倍の7連撃。防壁を蹴散らす王者の突進。";
                waitTime = 30;
                power = 11;
                cost = 0;
                attributes.Add(Effect.Attribute.KING);
                effects.Add(new Effect(TARGET_SINGLE, (int)(power * 0.5), DAMAGE, 30, attributes));
                effects.Add(new Effect(TARGET_SINGLE, (int)(power * 0.6), DAMAGE, 30, attributes));
                effects.Add(new Effect(TARGET_SINGLE, (int)(power * 0.7), DAMAGE, 30, attributes));
                effects.Add(new Effect(TARGET_SINGLE, (int)(power * 0.8), DAMAGE, 30, attributes));
                effects.Add(new Effect(TARGET_SINGLE, (int)(power * 0.9), DAMAGE, 30, attributes));
                effects.Add(new Effect(TARGET_SINGLE, (int)(power * 1.1), DAMAGE, 30, attributes));
                effects.Add(new Effect(TARGET_SINGLE, (int)(power * 1.7), DAMAGE, 30, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "四爪":
                description = "ランダムな敵に合計4回攻撃を行う。一回ごとの威力は低い。";
                waitTime = 13;
                power = 14;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 30));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 30));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 30));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 30));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "一閃":
                description = "大振りの攻撃で敵全体にダメージ。やや隙が大きいが威力も高い。";
                waitTime = 15;
                power = 71;
                cost = 0;
                effects.Add(new Effect(TARGET_ALL, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "火炎":
                description = "魔力の炎で敵単体に大ダメージ。魔法ダメージは防御を貫通する。";
                waitTime = 14;
                power = 82;
                cost = 15;
                attributes.Add(Effect.Attribute.MAGIC);
                attributes.Add(Effect.Attribute.FIRE);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "治癒":
                description = "癒しの魔力を開放し、自身のHPを即座に(40+魔力Lv*16)程度回復する。";
                waitTime = 14;
                power = 40;
                cost = 10;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(ME, power, HEAL, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "癒陣":
                description = "発動時わずかに味方全体を回復しつつ、3ターンの間継続的に回復を続ける陣を生成。";
                waitTime = 12;
                power = 10;
                cost = 20;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(ALLY_ALL, power, HEAL));
                effects.Add(new Effect(ALLY_ALL, power, Buff.BuffID.REGENERATE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "防御":
                description = "物理障壁を周囲に展開し、次に受ける物理ダメージを(防御Lv依存で)50%以上軽減する。";
                waitTime = 9;
                cost = 0;
                effects.Add(new Effect(ME, power, Buff.BuffID.GUARD));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "双盾":
                description = "物理障壁を一度に2枚展開する。防壁の維持できる枚数は防御Lvで決まる。";
                waitTime = 11;
                cost = 10;
                effects.Add(new Effect(ME, power, Buff.BuffID.GUARD));
                effects.Add(new Effect(ME, power, Buff.BuffID.GUARD));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "毒霧":
                description = "敵全体にわずかな魔法ダメージを与え、毒状態にする。";
                waitTime = 12;
                power = 24;
                cost = 20;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_ALL, power, DAMAGE));
                effects.Add(new Effect(TARGET_ALL, power, Buff.BuffID.POISON));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "吸収":
                description = "敵一体の生命力に直接干渉し、一部を奪い取る。奪い取った分の半分を回復する。";
                waitTime = 20;
                power = 70;
                cost = 20;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                effects.Add(new Effect(ME, power / 2, HEAL));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "雷光":
                description = "合計4回、ランダムな敵に強力な魔法ダメージを与える。";
                waitTime = 27;
                power = 85;
                cost = 50;
                attributes.Add(Effect.Attribute.THUNDER);
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 50, attributes));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 50, attributes));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 50, attributes));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE, 50, attributes));
                return new Action(actionName,description, waitTime, cost, effects, actor);
            case "力溜":
                description = "しばらく筋力Lv+3。";
                waitTime = 13;
                cost = 20;
                effects.Add(new Effect(ME, power, Buff.BuffID.STRUP));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "集中":
                description = "しばらく魔力Lv+3。";
                waitTime = 13;
                cost = 20;
                effects.Add(new Effect(ME, power, Buff.BuffID.INTUP));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "響火":
                description = "槍に火の魔力をまとわせ、物理攻撃のあとに火属性ダメージを追加。";
                waitTime = 13;
                cost = 40;
                effects.Add(new Effect(ME, power, Buff.BuffID.ENCHANT_FIRE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "加速":
                description = "しばらく速度Lv+3。";
                waitTime = 13;
                cost = 40;
                effects.Add(new Effect(ME, power, Buff.BuffID.SPDUP));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "幻閃":
                description = "ターン開始時に今ターゲットしている敵に対して攻撃。";
                waitTime = 17;
                cost = 20;
                effects.Add(new Effect(ME, power, Buff.BuffID.ADDITIONAL_ATTACK));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "追幻":
                description = "ターン終了時にランダムな敵に対して強力な攻撃。";
                waitTime = 18;
                cost = 20;
                effects.Add(new Effect(ME, power, Buff.BuffID.ADDITIONAL_ENDATTACK));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "幻閃起動":
                description = "幻閃による追撃。";
                waitTime = 0;
                power = 61;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "追幻起動":
                description = "追幻による追撃。";
                waitTime = 0;
                power = 129;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "衝撃":
                description = "魔力の塊をそのままぶつけて爆発させる。仕組みが単純なので高速に唱えられるが燃費が悪い。";
                waitTime = 6;
                power = 31;
                cost = 15;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "破防":
                description = "気合(魔力)を入れて敵の防壁を全部破壊しつつ物理攻撃。";
                waitTime = 14;
                power = 77;
                cost = 10;
                attributes.Add(Effect.Attribute.REMOVE_SHIELD);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "魔槍":
                description = "魔力で体の動きをブーストして一撃を叩き込む。";
                waitTime = 11;
                power = 115;
                cost = 15;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "残像":
                description = "ターン開始時に防壁を1枚追加。";
                waitTime = 10;
                cost = 20;
                effects.Add(new Effect(ME, power, Buff.BuffID.AUTO_SHIELD));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "鏡射":
                description = "ターン終了時に現在ターゲットしている敵に魔法ダメージ。";
                waitTime = 10;
                cost = 10;
                effects.Add(new Effect(ME, power, Buff.BuffID.ADDITIONAL_MAGIC));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "鏡射起動":
                description = "鏡射による追撃。";
                waitTime = 0;
                power = 69;
                cost = 0;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "凍結":
                description = "敵全体に強力な魔法ダメージを与える。";
                waitTime = 14;
                power = 75;
                cost = 30;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_ALL, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "烈風":
                description = "敵単体に強力な魔法ダメージを与える。";
                waitTime = 12;
                power = 101;
                cost = 10;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "瞬突":
                description = "すばやく正確な一撃を加える。";
                waitTime = 8;
                power = 61;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "獄炎":
                description = "敵全体に最高位の魔法ダメージを与える。";
                waitTime = 26;
                power = 215;
                cost = 99;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_ALL, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "霊祈":
                description = "ターン終了時にMPを回復。残りMPが少ないほど効果的。";
                waitTime = 10;
                cost = 20;
                effects.Add(new Effect(ME, power, Buff.BuffID.REGENERATE_MP));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "集霊":
                description = "MPを40回復する。";
                waitTime = 20;
                power = 40;
                cost = 0;
                effects.Add(new Effect(ME, power,Effect.EffectType.CONSTANTMPHEAL));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "高揚":
                description = "与えるダメージが2倍になる。";
                waitTime = 15;
                cost = 25;
                effects.Add(new Effect(ME, power, Buff.BuffID.EXALTED));
                return new Action(actionName, description, waitTime, cost, effects, actor);


            //******************
            //敵限定アクション
            //******************
            case "ひっかき":
                description = "ねこのスタンダード攻撃。";
                waitTime = 99;
                power = 14;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "もくもく":
                description = "もくもくと曖昧なダメージ。";
                waitTime = 99;
                power = 25;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "打撃":
                description = "攻撃。";
                waitTime = 99;
                power = 21;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "爪":
                description = "両手でひっかいてくる。";
                waitTime = 99;
                power = 6;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "ねこパンチ":
                description = "ねこの本気パンチ。いたい！";
                waitTime = 99;
                power = 38;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "するどい爪":
                description = "両手でひっかいてくる。二回ダメージ。";
                waitTime = 99;
                power = 17;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "強パンチ":
                description = "ねこの会心の一撃。少し痛い。";
                waitTime = 99;
                power = 42;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "てしてし":
                description = "じゃれてくる。全然痛くない";
                waitTime = 99;
                power = 2;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "噛みつく":
                description = "強力な顎で噛み付いてくる。";
                waitTime = 99;
                power = 63;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "毒牙":
                description = "毒を持った牙による噛みつき。毒状態になる。";
                waitTime = 99;
                power = 34;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE, 99, Buff.BuffID.POISON));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "強化":
                description = "一時的に身体を強化する。筋力魔力収魔速度防御を1Lv上昇。";
                waitTime = 10;
                power = 34;
                cost = 0;
                effects.Add(new Effect(ME, 99, Buff.BuffID.POWERUP));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "魔王剣":
                description = "魔王だから剣も使えます。";
                waitTime = 99;
                power = 74;
                cost = 0;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "静電気":
                description = "ピリッとしてくる。";
                waitTime = 8;
                power = 17;
                cost = 10;
                attributes.Add(Effect.Attribute.MAGIC);
                attributes.Add(Effect.Attribute.THUNDER);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE,60,attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "プチ火炎":
                description = "小規模な火炎攻撃。詠唱中に攻撃を当てることで妨害できる。";
                waitTime = 13;
                power = 51;
                cost = 10;
                attributes.Add(Effect.Attribute.MAGIC);
                attributes.Add(Effect.Attribute.FIRE);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "大凍結":
                description = "本気で凍らせてくる。";
                waitTime = 27;
                power = 96;
                cost = 10;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "障壁展開":
                description = "周囲の味方に障壁を配る。";
                waitTime = 10;
                power = 999;
                cost = 10;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(ALLY_ALL, 99, Buff.BuffID.GUARD, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            case "二重障壁":
                description = "2枚の障壁を展開。";
                waitTime = 10;
                power = 999;
                cost = 10;
                attributes.Add(Effect.Attribute.MAGIC);
                effects.Add(new Effect(ME, 99, Buff.BuffID.GUARD, 60, attributes));
                effects.Add(new Effect(ME, 99, Buff.BuffID.GUARD, 60, attributes));
                return new Action(actionName, description, waitTime, cost, effects, actor);
            default:
                Debug.LogWarning("getActionByNameのdefault:が呼ばれてる");
                return new Action();
        }

    }
}
