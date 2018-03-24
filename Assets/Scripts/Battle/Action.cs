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

    public Action()
    {
        //EnemyActionは派生型コンストラクタ経由でこれを呼ぶのでWarningなかったことに
        //Debug.LogWarning("Actionクラスのデフォルトコンストラクタが呼ばれてる");
        actionName = "アタック(by defaultConstructor)";
        descriptionText = "駄目な感じのアタック。これが呼ばれてはいけない";
        waitTime = 35;
        cost = 0;
        effectList = new List<Effect>();
        effectList.Add(new Effect());
    }
    //2つ以上の効果を持つアクション
    public Action(string actionName, string descriptionText, int waitTime, int cost, List<Effect> effectList, BattleCharacter actor=null)
    {
        this.actionName = actionName;
        this.descriptionText = descriptionText;
        this.waitTime = waitTime;
        this.cost = cost;
        this.effectList = effectList;

        //行動キャラが明示されている場合にはウェイトにそのキャラの速度Lvを反映
        if (actor != null)
        {
            this.waitTime = (int)(waitTime * actor.getWaitTimeCutRate());
        }
    }

}
