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
    //この効果は敵一体を狙うのか、味方全体なのか
    public TargetType targetType;

    //ベース威力(この値ベースに最終ダメージ量はキャラによって増減)
    public int effectAmount;

    //回復なのか攻撃なのか状態異常付与なのか
    public EffectType effectType;

    //バフ・状態異常なら付与するバフ名(バフ型作っちゃってもいいやも)
    public string buffName;

    public Effect()
    {
        targetType = TargetType.TARGET_SINGLE;
        effectAmount = 9;
        effectType = EffectType.DAMAGE;   
    }
    //ダメージ・回復の場合
    public Effect(TargetType ttype, int eAmount, EffectType etype)
    {
        targetType = ttype;
        effectAmount = eAmount;
        effectType = etype;
    }
    //バフかける場合
    public Effect(TargetType ttype, int eAmount, EffectType etype=EffectType.BUFF, string bName="")
    {
        targetType = ttype;
        effectAmount = eAmount;
        effectType = etype;
        buffName = bName;
    }
}
