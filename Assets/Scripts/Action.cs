using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Actionは、「サンダー」とか「こうげき」とかプレイヤーが能動的に行う行動一つを表す
//Actionは一つ以上の効果(Effect)=敵一体にダメージ、ランダムな敵に3回雷を落とす...を持つ
public class Action{
    public string actionName;
    public int waitTime;
    public int cost;
    //この行動の効果一覧
    //(敵一体に攻撃 + 状態異常付与とか...)
    public List<Effect> effectList;

    public Action()
    {
        Debug.LogWarning("Actionクラスのデフォルトコンストラクタが呼ばれてる");
        actionName = "アタック(by defaultConstructor)";
        waitTime = 35;
        cost = 0;
        effectList = new List<Effect>();
        effectList.Add(new Effect());
    }
    //2つ以上の効果を持つアクション
    public Action(string actionName, int waitTime, int cost, List<Effect> effectList)
    {
        this.actionName = actionName;
        this.waitTime = waitTime;
        this.cost = cost;
        this.effectList = effectList;
    }
    //単発の効果で終わるアクション
    public Action(string actionName, int waitTime, int cost, Effect effect)
    {
        this.actionName = actionName;
        this.waitTime = waitTime;
        this.cost = cost;
        effectList = new List<Effect>();
        effectList.Add(effect);
    }

}
