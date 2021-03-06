﻿using System.Collections;
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
        CONSTANTDAMAGE,
        HEAL,
        CONSTANTHEAL,
        CONSTANTEXCEEDHEAL,
        MPHEAL,
        CONSTANTMPHEAL,
        CONSTANTEXCEEDMPHEAL,
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
        PETIT_KING, //ぷちビクトリー 3ターン目以降発動
        KING, //ビクトリー 5ターン目以降発動
        SKIP, //即系 初手のみつよい
        PETIT_SKIP,//プチ即 2ターン目まで強い
        REMOVE_SHIELD, //盾を全部壊す
    }

    //もともとどのアクションの行動だったのか
    public string actionName;

    //この効果は敵一体を狙うのか、味方全体なのか
    public TargetType targetType;

    //ベース威力(この値ベースに最終ダメージ量はキャラによって増減)
    public int effectAmount;

    //この効果の再生時に停止する時間
    public int blockingFrames;

    //回復なのか攻撃なのか状態異常付与なのか
    public EffectType effectType;

    //バフ・状態異常なら付与するバフ名(バフ型作っちゃってもいいやも)
    public Buff.BuffID buffID;

    //効果属性 龍特攻とか
    public List<Attribute> attributes;

    public bool hasAttribute(Attribute attr)
    {
        return attributes.Contains(attr);
    }

    public Effect()
    {
        targetType = TargetType.TARGET_SINGLE;
        effectAmount = 9;
        effectType = EffectType.DAMAGE;
        attributes = new List<Attribute>();
    }
    //ダメージ・回復の場合
    public Effect(string actionName, TargetType targetType, int effectAmount, EffectType effectType, int blockingFrames=60, List<Attribute> attributes=null)
    {
        this.targetType = targetType;
        this.effectAmount = effectAmount;
        this.effectType = effectType;
        this.blockingFrames = blockingFrames;
        this.actionName = actionName;
        
        if (attributes != null)
        {
            this.attributes = attributes;
        }
        else
        {
            this.attributes = new List<Attribute>();
        }
    }
    //バフかける場合
    public Effect(string actionName, TargetType targetType, int effectAmount, Buff.BuffID buffID, int blockingFrames =30, List<Attribute> attributes=null)
    {
        this.targetType = targetType;
        this.effectAmount = effectAmount;
        this.effectType = EffectType.BUFF;
        this.blockingFrames = blockingFrames;
        this.buffID = buffID;
        this.actionName = actionName;
        if (attributes != null)
        {
            this.attributes = attributes;
        }
        else
        {
            this.attributes = new List<Attribute>();
        }
    }
}
