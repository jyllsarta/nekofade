using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EffectはActionが引き起こす単一の行動
//敵全体にダメージ、敵一体を状態異常にする... これの組み合わせでActionを構成する
public class Effect{
    //敵の選び方 敵一体/敵ランダム一体/敵全体/自分/
    public enum TargetType
    {
        TARGET_SINGLE,
        TARGET_SINGLE_RANDOM,
        TARGET_ALL,
        ME,
        ALLY_SINGLE_RANDOM,
        ALLY_ALL,
    }
    //何をする効果なのか
    public enum EffectType
    {
        DAMAGE,
        HEAL,
        BUFF,
    }

    //属性(アトリビュート)
    //一つのエフェクトにつき複数のアトリビュートが存在しうる
    public enum Attribute
    {
        PHISICAL, //物理 明示せずとも基本的には物理攻撃として扱うが
        MAGIC, //魔法
        FIRE,
        THUNDER,
        ANIMAL_SLAYER,
    }

    //この効果は敵一体を狙うのか、味方全体なのか
    public TargetType targetType;

    //ベース威力(この値ベースに最終ダメージ量はキャラによって増減)
    public int effectAmount;

    //回復なのか攻撃なのか状態異常付与なのか
    public EffectType effectType;

    //バフ・状態異常なら付与するバフ名(バフ型作っちゃってもいいやも)
    public Buff.BuffID buffID;

    public List<Attribute> attributes;

    public Effect()
    {
        targetType = TargetType.TARGET_SINGLE;
        effectAmount = 9;
        effectType = EffectType.DAMAGE;
        attributes = new List<Attribute>();
    }
    //ダメージ・回復の場合
    public Effect(TargetType ttype, int eAmount, EffectType etype, List<Attribute> attributes=null)
    {
        targetType = ttype;
        effectAmount = eAmount;
        effectType = etype;
        if (attributes != null)
        {
            this.attributes = attributes;
        }
        else
        {
            attributes = new List<Attribute>();
        }
    }
    //バフかける場合
    public Effect(TargetType targetType, int effectAmount, Buff.BuffID buffID, List<Attribute> attributes=null)
    {
        this.targetType = targetType;
        this.effectAmount = effectAmount;
        this.effectType = EffectType.BUFF;
        this.buffID = buffID;
        if (attributes != null)
        {
            this.attributes = attributes;
        }
        else
        {
            attributes = new List<Attribute>();
        }
    }
}
