using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    public BattleCharacter player;
    public List<BattleCharacter> enemies;

    //今ターゲットしてる敵の番号
    public int currentTargettingEnemyIndex;

    public enum ActorType
    {
        ENEMY,
        PLAYER,
    }

	// Use this for initialization
	void Start () {
        currentTargettingEnemyIndex = 0;
        Debug.Log("ぬ");
	}

    //生きてる中で一番近いやつをターゲットする
    int getIndexOfActiveEnemy()
    {
        for (int i=0;i< enemies.Count; ++i)
        {
            if (!enemies[i].isDead())
            {
                return i;
            }
        }
        Debug.LogError("生きてる敵探しましたが敵全部死んでますよ");
        return 0;
    }

    void checkBattleFinish()
    {
        if (player.isDead())
        {
            Debug.Log("まけ");
        }
        if (enemies[0].isDead())
        {
            Debug.Log("勝ち");
        }

    }

    //実際のEffect一つの処理
    void consumeEffect(BattleCharacter actor, ref BattleCharacter target, Effect effect)
    {
        switch (effect.effectType)
        {
            case Effect.EffectType.DAMAGE:
                //TODO ダメージ計算式
                target.hp -= effect.effectAmount;
                break;
            case Effect.EffectType.HEAL:
                target.hp += effect.effectAmount;
                break;
            case Effect.EffectType.BUFF:
                //TODO 今後成功率とかやるかも
                Debug.Log("バフ乗せは未実装");
                break;
        }
    }

    //どのアクションを、 敵と味方どっちの、何人目が行うか
    //味方の場合actorIndexは自明に0なので省略可
    public void consumeAction(Action action, ActorType actortype, int actorIndex=0)
    {
        foreach(Effect effect in action.effectList)
        {
            switch (effect.targetType)
            {
                //敵リストのインデックス指定を行っているところ、
                //list内の要素はrefで渡せないのでコピーして渡して結果を戻してる
                //遅くなったら対策を考えよう...

                case Effect.TargetType.ALLY_ALL:
                    switch (actortype)
                    {
                        case ActorType.PLAYER:
                            //自分が味方全体行動→自分自身のみ
                            consumeEffect(player, ref player, effect);
                            break;
                        case ActorType.ENEMY:
                            //敵が味方全体行動 → 敵全体になにかが起こる
                            for (int i = 0; i < enemies.Count; ++i)
                            {
                                BattleCharacter target = enemies[i];
                                consumeEffect(enemies[actorIndex], ref target, effect);
                                enemies[i] = target;
                            }
                            break;
                    }
                    break;
                case Effect.TargetType.ALLY_SINGLE_RANDOM:
                    switch (actortype)
                    {
                        case ActorType.PLAYER:
                            //自分が味方ランダム行動→自分自身のみ
                            consumeEffect(player, ref player, effect);
                            break;
                        case ActorType.ENEMY:
                            //敵が味方ランダム行動 → 敵のうちどれか
                            int targetIndex = Random.Range(0, enemies.Count);
                            BattleCharacter target = enemies[targetIndex];
                            consumeEffect(enemies[actorIndex], ref target, effect);
                            enemies[targetIndex] = target;
                            break;
                    }
                    break;
                case Effect.TargetType.ME:
                    switch (actortype)
                    {
                        case ActorType.PLAYER:
                            //自分が自分自身
                            Debug.Log("自分になんかした");
                            consumeEffect(player, ref player, effect);
                            break;
                        case ActorType.ENEMY:
                            //敵が自分自身
                            BattleCharacter target = enemies[actorIndex];
                            consumeEffect(enemies[actorIndex], ref target, effect);
                            enemies[actorIndex] = target;
                            break;
                    }
                    break;
                case Effect.TargetType.TARGET_ALL:
                    switch (actortype)
                    {
                        case ActorType.PLAYER:
                            //敵全部をちまちま殴る
                            for (int i=0;i<enemies.Count;++i)
                            {
                                BattleCharacter target = enemies[i];
                                consumeEffect(player, ref target, effect);
                                enemies[i] = target;
                            }
                            break;
                        case ActorType.ENEMY:
                            //敵が全体攻撃 → 味方は一人なので自分狙い
                            consumeEffect(enemies[actorIndex], ref player, effect);
                            break;
                    }
                    break;
                case Effect.TargetType.TARGET_SINGLE:
                    switch (actortype)
                    {
                        case ActorType.PLAYER:
                            //自分が敵一体狙う → 今のターゲットを狙う
                            BattleCharacter target = enemies[currentTargettingEnemyIndex];
                            consumeEffect(player, ref target, effect);
                            enemies[currentTargettingEnemyIndex] = target;
                            break;
                        case ActorType.ENEMY:
                            //敵が敵一体狙う → 自分狙い
                            consumeEffect(enemies[actorIndex], ref player, effect);
                            break;
                    }
                    break;
                case Effect.TargetType.TARGET_SINGLE_RANDOM:
                    switch (actortype)
                    {
                        case ActorType.PLAYER:
                            //自分が敵ランダムを狙う
                            int targetIndex = Random.Range(0, enemies.Count);
                            BattleCharacter target = enemies[targetIndex];
                            consumeEffect(player, ref target, effect);
                            enemies[targetIndex] = target;
                            break;
                        case ActorType.ENEMY:
                            //敵がランダムで狙ってくる → 味方は一人なので自分狙い
                            consumeEffect(enemies[actorIndex], ref player, effect);
                            break;
                    }
                    break;
            }
        
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Action act = ActionStore.getActionByName("");
            consumeAction(act, ActorType.PLAYER, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Action act = ActionStore.getActionByName("衝波");
            consumeAction(act, ActorType.PLAYER, 0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Action act = ActionStore.getActionByName("火炎");
            consumeAction(act, ActorType.PLAYER, 0);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Action act = ActionStore.getActionByName("治癒");
            consumeAction(act, ActorType.PLAYER, 0);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Action act = ActionStore.getActionByName("吸収");
            consumeAction(act, ActorType.PLAYER, 0);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Action act = ActionStore.getActionByName("轟雷");
            consumeAction(act, ActorType.PLAYER, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Action act = ActionStore.getActionByName("");
            consumeAction(act, ActorType.ENEMY, 0);
        }
        checkBattleFinish();
    }
}
