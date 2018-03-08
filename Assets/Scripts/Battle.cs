using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    public BattleCharacter player;
    public List<BattleCharacter> enemies;
    public EnemyStore enemyStore;
    public Timeline timeline;
    public GameObject enemiesUI;
    public GameObject targetCircle;

    //今ターゲットしてる敵の番号
    public int currentTargettingEnemyIndex;

    //バトルシーンの状態　プレイヤーのコマンド受付なのかバトル中なのか
    public GameState currentGameState;

    public enum GameState
    {
        PLAYER_THINK,
        TURN_PROCEEDING,
        EFFECT_ANIMATION_WAITING,
    }

    public enum ActorType
    {
        ENEMY,
        PLAYER,
    }

	// Use this for initialization
	void Start () {
        setEnemy();
        currentTargettingEnemyIndex = 0;
        currentGameState = GameState.PLAYER_THINK;
	}

    //デバッグ用 適当に敵を置く
    void setEnemy()
    {
        for (int i=0;i<3;++i)
        {
            BattleCharacter enemy;
            enemy = enemyStore.getEnemyByName("scp");
            enemy.transform.SetParent(enemiesUI.transform);
            enemy.transform.localPosition = new Vector3((i-1)*400, (1 - i) * 30, 0);
            enemy.battle = this;
            enemies.Add(enemy);
        }
    }

    //このハッシュコードを持つ敵は何番目だ
    int getEnemyIndexByHashCode(int hashCode)
    {
        for (int i=0;i<enemies.Count; ++i)
        {
            if (enemies[i].GetHashCode() == hashCode)
            {
                return i;
            }
        }
        Debug.LogWarning("getEnemyIndexByHashCode失敗したけど大丈夫かな");
        return -1;
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

    public void targetEnemyByHash(int hashCode)
    {
        Debug.LogFormat("{0}!これね",hashCode);

        for (int i = 0; i < enemies.Count; ++i)
        {
            if (enemies[i].GetHashCode() == hashCode)
            {
                currentTargettingEnemyIndex = i;
                targetCircle.SetActive(true);
                targetCircle.transform.position = enemies[i].transform.position;
                return;
            }
        }
        Debug.Log("もしや");
    }

    void checkBattleFinish()
    {
        //プレイヤーが死んだら負け
        if (player.isDead())
        {
            Debug.Log("まけ");
        }
        //敵を全滅させたら勝ち
        if (enemies.TrueForAll((e)=>e.isDead()))
        {
            Debug.Log("勝ち");
        }

    }

    //ダメージ計算
    int calcDamage(BattleCharacter actor, BattleCharacter target, Effect effect)
    {
        //ダメージ倍率
        float multiply = 1.0f;

        //雷→水チェック
        if (target.hasAttribute(CharacterAttribute.AttributeID.WATER) && effect.hasAttribute(Effect.Attribute.THUNDER))
        {
            multiply *= 2;
            Debug.Log("雷特効！");
        }


        //効果量に倍率かけて端数落としたものを最終ダメージとする
        int finalDamage = (int)(effect.effectAmount * multiply);

        return finalDamage;
    }

    //実際のEffect一つの処理
    void consumeEffect(BattleCharacter actor, ref BattleCharacter target, Effect effect)
    {
        switch (effect.effectType)
        {
            case Effect.EffectType.DAMAGE:
                int damage = calcDamage(actor, target, effect);
                target.hp -= damage;
                break;
            case Effect.EffectType.HEAL:
                target.hp += calcDamage(actor, target, effect);
                break;
            case Effect.EffectType.BUFF:
                //一旦確定付与
                target.buffs.Add(BuffStore.getBuffByBuffID(effect.buffID));
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

    //ターンの初めの処理
    public void onTurnStart()
    {
        timeline.newTurn();
        currentGameState = GameState.PLAYER_THINK;
        putEnemyAction();

    }

    public void turnEnd()
    {
        currentGameState = GameState.TURN_PROCEEDING;
    }

    //このフレームに積んであるアクションを実行
    public void playActionCurrentFrame()
    {
        //プレイヤー行動
        Action a = timeline.getActionByFrame(timeline.currentFrame);
        if (a != null)
        {
            consumeAction(a, ActorType.PLAYER);
            //キルとったかもしれないからリターゲット
            currentTargettingEnemyIndex = getIndexOfActiveEnemy();
        }
        //敵行動
        EnemyAction ea = timeline.getEnemyActionByFrame(timeline.currentFrame);
        if (ea != null)
        {
            consumeAction(ea, ActorType.ENEMY,getEnemyIndexByHashCode(ea.actorHash));
        }
    }

    //生きてる敵が雑に行動を積む
    void putEnemyAction()
    {
        foreach (BattleCharacter enemy in enemies)
        {
            EnemyAction a = new EnemyAction(ActionStore.getActionByName(enemy.actions[0]));
            a.actorHash = enemy.GetHashCode();
            a.frame = Random.Range(1, timeline.framesPerTurn);
            timeline.addEnemyAction(a);
        }
    }

    void debugInputAction()
    {
        //キー入力で強制コマンド実行
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Action act = ActionStore.getActionByName("刺突");
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
            Action act = ActionStore.getActionByName("雷光");
            consumeAction(act, ActorType.PLAYER, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Action act = ActionStore.getActionByName("");
            consumeAction(act, ActorType.ENEMY, 0);
        }
    }

    // Update is called once per frame
    void Update(){
            checkBattleFinish();
            debugInputAction();
        switch (currentGameState)
        {
            case GameState.PLAYER_THINK:
                break;
            case GameState.TURN_PROCEEDING:
            case GameState.EFFECT_ANIMATION_WAITING:
                timeline.proceed(); //これだと0フレーム目にアクションおかれたらすかされる 大丈夫か検討
                playActionCurrentFrame();
                if (timeline.currentFrame == timeline.framesPerTurn)
                {
                    onTurnStart();
                }
                break;
        }
    }
}
