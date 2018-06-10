using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Actionは、「サンダー」とか「こうげき」とかプレイヤーが能動的に行う行動一つを表す
//Actionは一つ以上の効果(Effect)=敵一体にダメージ、ランダムな敵に3回雷を落とす...を持つ
public class Action{
    public string actionName;
    public string descriptionText;
    public int waitTime;
    public int cost;
    //この行動の効果一覧
    //(敵一体に攻撃 + 状態異常付与とか...)
    public List<Effect> effectList;

    public Rarity rarity;

    public enum Rarity
    {
        COMMON,
        RARE,
        EPIC,
        LEGENDARY,
    }

    public static int getCostByRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.COMMON:
                return 200;
            case Rarity.RARE:
                return 300;
            case Rarity.EPIC:
                return 500;
            case Rarity.LEGENDARY:
                return 800;
            default:
                Debug.LogWarning("getCostByRarityのdefaultが呼ばれてる");
                return 999;
        }
    }

    public Action()
    {
        //EnemyActionは派生型コンストラクタ経由でこれを呼ぶのでWarningなかったことに
        //Debug.LogWarning("Actionクラスのデフォルトコンストラクタが呼ばれてる");
        actionName = "アタック(by defaultConstructor)";
        descriptionText = "駄目な感じのアタック。これが呼ばれてはいけない";
        waitTime = 35;
        cost = 0;
        rarity = Rarity.COMMON;
        effectList = new List<Effect>();
        effectList.Add(new Effect());
    }
    //2つ以上の効果を持つアクション
    public Action(string actionName, string descriptionText, int waitTime, int cost, List<Effect> effectList, Rarity rarity=Rarity.COMMON, BattleCharacter actor=null)
    {
        this.actionName = actionName;
        this.descriptionText = descriptionText;
        this.waitTime = waitTime;
        this.cost = cost;
        this.effectList = effectList;
        this.rarity = rarity;

        //行動キャラが明示されている場合にはウェイトにそのキャラの速度Lvを反映
        if (actor != null)
        {
            this.waitTime = (int)(waitTime * actor.getWaitTimeCutRate());
        }
    }

    public bool isMagic()
    {
        return effectList.Exists(x => x.hasAttribute(Effect.Attribute.MAGIC));
    }

}
