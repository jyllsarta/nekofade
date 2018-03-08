﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵のとるアクション プレイヤーのアクションとは別物になっちゃうけどまあいいや
//いつ誰がやるアクションなのか追加で持ってます
public class EnemyAction : Action{

    //この技の発動するフレーム
    public int frame;

    //行動するキャラのハッシュコード
    public int actorHash;

    //コピーして作るコンストラクタ(ちょっとダメ設計かも)
    public EnemyAction(Action a)
    {
        this.actionName = a.actionName;
        this.waitTime = a.waitTime;
        this.cost = a.cost;
        this.effectList = a.effectList;
    }
}