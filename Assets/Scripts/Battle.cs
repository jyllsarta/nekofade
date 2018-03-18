using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour {
    public BattleCharacter player;
    public List<BattleCharacter> enemies;
    public EnemyStore enemyStore;
    public BuffStore buffStore;
    public Timeline timeline;
    public GameObject enemiesUI;
    public GameObject targetCircle;

    public DamageEffect damageEffect;
    public DamageEffect healEffect;

    //エフェクトキュー
    public Queue<PlayableEffect> effectQueue;

    //あと何フレームエフェクト再生で止まるか
    public int remainingEffectAnimationframes;

    //今ターゲットしてる敵の番号
    public int currentTargettingEnemyIndex;

    //バトルシーンの状態　プレイヤーのコマンド受付なのかバトル中なのか
    public GameState currentGameState;

    public enum GameState
    {
        PLAYER_THINK,
        TURN_PROCEEDING,
        EFFECT_WAITING,
    }

    public enum ActorType
    {
        ENEMY,
        PLAYER,
    }

	// Use this for initialization
	void Start () {
        currentTargettingEnemyIndex = 0;
        effectQueue = new Queue<PlayableEffect>();
        currentGameState = GameState.PLAYER_THINK;
	}

    //リストの内容に従って敵を置く
    public void setEnemy(List<string> enemyList)
    {
        int enemysize = enemyList.Count;

        for(int i=0;i<enemyList.Count;++i)
        {
            BattleCharacter enemy;
            enemy = enemyStore.instanciateEnemyByName(enemyList[i], enemiesUI.transform);
            enemy.transform.Translate(new Vector3((i-1)*250, (1 - i) * 10, 0));
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
        //enemiesは死んだ敵を保持しなくなったので最初の要素が常に「生きてる中で一番近いやつ」になる
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
            //掃除 今後これが正しいかどうかはともかくとりあえず置いておく
            SirokoStats s = FindObjectOfType<SirokoStats>();
            if (s)
            {
                Destroy(s.gameObject);
            }
            SceneManager.LoadScene("debugBattleSimulator");

        }
        //敵を全滅させたら勝ち
        if (enemies.TrueForAll((e)=>e.isDead()))
        {
            Debug.Log("勝ち");
            //掃除 今後これが正しいかどうかはともかくとりあえず置いておく
            SirokoStats s = FindObjectOfType<SirokoStats>();
            if (s)
            {
                Destroy(s.gameObject);
            }
            SceneManager.LoadScene("debugBattleSimulator");
        }

    }

    //ダメージ計算
    int calcDamage(BattleCharacter actor, BattleCharacter target, Effect effect)
    {
        //ダメージ倍率
        float multiply = 1.0f;

        //攻撃倍率を乗算
        //魔法攻撃は魔力依存
        if (effect.hasAttribute(Effect.Attribute.MAGIC))
        {
            multiply *= actor.getMagicRate() ;
        }
        //魔法攻撃と明示されているもの以外はすべて物理攻撃である
        else
        {
            multiply *= actor.getAttackRate();

            //物理攻撃は相手側の防御Lvに応じてダメージカット
            multiply *= (1f - target.getNormalCutRate());
        }


        //相手が防御してたら防御回数を減らしつつダメージ減衰
        if (target.hasShield())
        {
            multiply *= (1f-target.getDefenceCutRate());
            target.shieldCount -= 1;
            //Debug.LogFormat("防御！{0}%ダメージカット", target.getDefenceCutRate()*100);
        }

        //特効枠
        //王撃 5ターン目以降のみつよい
        if (effect.hasAttribute(Effect.Attribute.KING) && timeline.getTotalFrame() >= timeline.framesPerTurn * 4)
        {
            //Debug.Log("王撃！7倍ダメージ");
            multiply *= 7;
        }
        //ぷち王撃 3ターン目以降のみつよい
        if (effect.hasAttribute(Effect.Attribute.PETIT_KING) && timeline.getTotalFrame() >= timeline.framesPerTurn * 2)
        {
            //Debug.Log("ぷち王撃！3倍ダメージ");
            multiply *= 3;
        }
        //即撃 1ターン目のみつよい
        if (effect.hasAttribute(Effect.Attribute.SKIP) && timeline.getTotalFrame() <= timeline.framesPerTurn)
        {
            //Debug.Log("即撃！2.5倍ダメージ");
            multiply *= 2.5f;
        }
        //ぷち即撃 2ターン目までつよい
        if (effect.hasAttribute(Effect.Attribute.PETIT_SKIP) && timeline.getTotalFrame() <= timeline.framesPerTurn * 2)
        {
            //Debug.Log("ぷち即撃！1.5倍ダメージ");
            multiply *= 1.5f;
        }


        //雷→水チェック
        if (target.hasAttribute(CharacterAttribute.AttributeID.WATER) && effect.hasAttribute(Effect.Attribute.THUNDER))
        {
            multiply *= 2;
            //Debug.Log("雷特効！");
        }


        //効果量に倍率かけて端数落としたものを最終ダメージとする
        int finalDamage = (int)(effect.effectAmount * multiply);


        //Debug.LogFormat("{0}が{1}に{2}ダメージ!",actor.name,target.name,finalDamage);
        return finalDamage;
    }

    void resolveDamage(BattleCharacter actor, ref BattleCharacter target, int damage)
    {
        target.hp -= damage;
        //エフェクトの再生
        DamageEffect createdDamageEffect = Instantiate(damageEffect, target.transform);
        createdDamageEffect.damageText.text = damage.ToString();
        createdDamageEffect.transform.position = target.transform.position;
        target.playDamageAnimation();
    }

    void resolveHeal(BattleCharacter actor, ref BattleCharacter target, int value)
    {
        target.hp += value;
        if (target.hp > target.maxHp)
        {
            target.hp = target.maxHp;
        }
        //エフェクトの再生
        DamageEffect createdEffect = Instantiate(healEffect, target.transform);
        createdEffect.damageText.text = value.ToString();
        createdEffect.transform.position = target.transform.position;
    }

    //実際のEffect一つの処理
    void resolveEffect(BattleCharacter actor, ref BattleCharacter target, Effect effect)
    {
        switch (effect.effectType)
        {
            case Effect.EffectType.DAMAGE:
                int damage = calcDamage(actor, target, effect);
                resolveDamage(actor, ref target, damage);
                break;
            case Effect.EffectType.HEAL:
                int value = calcDamage(actor, target, effect);
                resolveHeal(actor, ref target, value);
                break;
            case Effect.EffectType.BUFF:
                enchantBuff(effect.buffID, ref target);
                break;
        }
    }

    public void enchantBuff(Buff.BuffID buffID, ref BattleCharacter target)
    {
        //防御だけは別で防御枚数の増加を行う
        if (buffID == Buff.BuffID.GUARD)
        {
            target.addShield();
        }
        else
        {
            Buff b = buffStore.instanciateBuffByBuffID(buffID, target.buffContainer.transform);
            b.text.text = b.isPermanent ? "∞" : b.length.ToString();
            //もうすでにそのバフを持ってて、それが重複しない場合は古いやつは消す
            if (target.hasBuff(b.buffID) && !b.duplicates)
            {
                target.removeBuff(b.buffID);
                //ここ実は結構危ない橋でややこしい挙動なので今度なんとかしたい
                //instanciateしたばかりのやつはbuffsにまだ載ってないからここで消しても大丈夫なのでremoveBuffを呼んでる
            }

            target.buffs.Add(b);
        }
    }

    //どっち陣営の誰がを指定してエフェクト一つを消化する
    void consumeEffect(PlayableEffect pe)
    {
        switch (pe.effect.targetType)
        {
            //敵リストのインデックス指定を行っているところ、
            //list内の要素はrefで渡せないのでコピーして渡して結果を戻してる
            //遅くなったら対策を考えよう...

            case Effect.TargetType.ALLY_ALL:
                switch (pe.actortype)
                {
                    case ActorType.PLAYER:
                        //自分が味方全体行動→自分自身のみ
                        resolveEffect(player, ref player, pe.effect);
                        break;
                    case ActorType.ENEMY:
                        //敵が味方全体行動 → 敵全体になにかが起こる
                        for (int i = 0; i < enemies.Count; ++i)
                        {
                            BattleCharacter target = enemies[i];
                            resolveEffect(enemies[pe.actorIndex], ref target, pe.effect);
                            enemies[i] = target;
                        }
                        break;
                }
                break;
            case Effect.TargetType.ALLY_SINGLE_RANDOM:
                switch (pe.actortype)
                {
                    case ActorType.PLAYER:
                        //自分が味方ランダム行動→自分自身のみ
                        resolveEffect(player, ref player, pe.effect);
                        break;
                    case ActorType.ENEMY:
                        //敵が味方ランダム行動 → 敵のうちどれか
                        int targetIndex = Random.Range(0, enemies.Count);
                        BattleCharacter target = enemies[targetIndex];
                        resolveEffect(enemies[pe.actorIndex], ref target, pe.effect);
                        enemies[targetIndex] = target;
                        break;
                }
                break;
            case Effect.TargetType.ME:
                switch (pe.actortype)
                {
                    case ActorType.PLAYER:
                        //自分が自分自身
                        Debug.Log("自分になんかした");
                        resolveEffect(player, ref player, pe.effect);
                        break;
                    case ActorType.ENEMY:
                        //敵が自分自身
                        BattleCharacter target = enemies[pe.actorIndex];
                        resolveEffect(enemies[pe.actorIndex], ref target, pe.effect);
                        enemies[pe.actorIndex] = target;
                        break;
                }
                break;
            case Effect.TargetType.TARGET_ALL:
                switch (pe.actortype)
                {
                    case ActorType.PLAYER:
                        //敵全部をちまちま殴る
                        for (int i = 0; i < enemies.Count; ++i)
                        {
                            BattleCharacter target = enemies[i];
                            resolveEffect(player, ref target, pe.effect);
                            enemies[i] = target;
                        }
                        break;
                    case ActorType.ENEMY:
                        //敵が全体攻撃 → 味方は一人なので自分狙い
                        resolveEffect(enemies[pe.actorIndex], ref player, pe.effect);
                        break;
                }
                break;
            case Effect.TargetType.TARGET_SINGLE:
                switch (pe.actortype)
                {
                    case ActorType.PLAYER:
                        //自分が敵一体狙う → 今のターゲットを狙う
                        BattleCharacter target = enemies[currentTargettingEnemyIndex];
                        resolveEffect(player, ref target, pe.effect);
                        enemies[currentTargettingEnemyIndex] = target;
                        break;
                    case ActorType.ENEMY:
                        //敵が敵一体狙う → 自分狙い
                        resolveEffect(enemies[pe.actorIndex], ref player, pe.effect);
                        break;
                }
                break;
            case Effect.TargetType.TARGET_SINGLE_RANDOM:
                switch (pe.actortype)
                {
                    case ActorType.PLAYER:
                        //自分が敵ランダムを狙う
                        int targetIndex = Random.Range(0, enemies.Count);
                        BattleCharacter target = enemies[targetIndex];
                        resolveEffect(player, ref target, pe.effect);
                        enemies[targetIndex] = target;
                        break;
                    case ActorType.ENEMY:
                        //敵がランダムで狙ってくる → 味方は一人なので自分狙い
                        resolveEffect(enemies[pe.actorIndex], ref player, pe.effect);
                        break;
                }
                break;
        }

    }

    //どのアクションを、 敵と味方どっちの、何人目が行うかを指定して実際の効果となるエフェクトをキューに積む
    //味方の場合actorIndexは自明に0なので省略可
    public void consumeAction(Action action, ActorType actortype, int actorIndex=0)
    {
        foreach(Effect effect in action.effectList)
        {
            effectQueue.Enqueue(new PlayableEffect(effect,actortype,actorIndex));
        }
        //今積んだエフェクト再生が終わるまでタイムラインは停止する
        currentGameState = GameState.EFFECT_WAITING;
    }

    //ターンの初めの処理
    public void onTurnStart()
    {
        timeline.newTurn();
        currentGameState = GameState.PLAYER_THINK;
        putEnemyAction();
        player.OnTurnStart();
        foreach (BattleCharacter e in enemies)
        {
            e.OnTurnStart();
        }

    }

    public void turnEnd()
    {
        currentGameState = GameState.TURN_PROCEEDING;
    }

    //全キャラの毎フレームごとの処理を呼ぶ
    public void triggerEveryCharacterEveryFrameEffect()
    {
        player.onEveryFrame();
        foreach (BattleCharacter e in enemies)
        {
            e.onEveryFrame();
        }
    }
    //全キャラのターンエンドごとの処理を呼ぶ
    public void triggerEveryCharacterTurnEndEffect()
    {
        player.OnTurnEnd();
        foreach (BattleCharacter e in enemies)
        {
            e.OnTurnEnd();
        }
    }

    //全キャラのバフを1Fぶん進める
    public void proceedEveryCharactersBuffState()
    {
        player.updateBuffState();
        foreach (BattleCharacter e in enemies)
        {
            e.updateBuffState();
        }
    }

    //このフレームに積んであるアクションを実行
    public void playActionCurrentFrame()
    {
        //プレイヤー行動
        Action a = timeline.getActionByFrame(timeline.currentFrame);
        if (a != null && !player.isDead())
        {
            consumeAction(a, ActorType.PLAYER);
        }
        //敵行動
        EnemyAction ea = timeline.getEnemyActionByFrame(timeline.currentFrame);
        if (ea != null)
        {
            int enemyIndex = getEnemyIndexByHashCode(ea.actorHash);
            //死んでなかったらやる
            if (enemyIndex != -1 && !enemies[enemyIndex].isDead())
            {
                consumeAction(ea, ActorType.ENEMY, enemyIndex);
            }
            else
            {
                Debug.LogFormat("{0}番目の敵は死んでるのでアクションを実行しませんでした",enemyIndex);
            }
        }
    }

    //生きてる敵が自分の行動を積む
    void putEnemyAction()
    {
        foreach (BattleCharacter enemy in enemies)
        {
            if (enemy.isDead())
            {
                continue;
            }
            string actionName = enemy.nextAction();
            EnemyAction a = new EnemyAction(ActionStore.getActionByName(actionName,enemy));
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

    void removeDeadEnemyFromScene()
    {
        foreach (BattleCharacter enemy in enemies)
        {
            if (enemy.isDead())
            {
                timeline.removeEnemyActionByHash(enemy.GetHashCode());
                enemy.playDeathAnimationThenRemoveFromScene();
            }
        }
        enemies.RemoveAll(x => x.isDead());
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
                timeline.proceed(); //これだと0フレーム目にアクションおかれたらすかされる 大丈夫か検討
                playActionCurrentFrame();
                triggerEveryCharacterEveryFrameEffect();
                proceedEveryCharactersBuffState();
                //最後のフレームで再生中エフェクトが無くなったらターン終わり
                if (timeline.currentFrame >= timeline.framesPerTurn && effectQueue.Count == 0)
                {
                    triggerEveryCharacterTurnEndEffect();
                    onTurnStart();
                }
                break;
            case GameState.EFFECT_WAITING:
                //再生中ならとりあえずそれが終了するまでエフェクトの再生を続けてもらう
                if (remainingEffectAnimationframes > 0)
                {
                    remainingEffectAnimationframes--;
                    return;
                }
                //再生終わり、まだエフェクトが残ってるなら次のエフェクトの再生に移る
                else if (effectQueue.Count > 0)
                {
                    PlayableEffect pe = effectQueue.Dequeue();
                    consumeEffect(pe);
                    remainingEffectAnimationframes = pe.blockingFrames;
                }
                //エフェクトの再生が終わり、キューにも残ってない場合
                else
                {
                    //このアクションで敵が死んだ場合にはリターゲット
                    if (enemies[currentTargettingEnemyIndex].isDead())
                    {
                        currentTargettingEnemyIndex = getIndexOfActiveEnemy();
                        targetCircle.SetActive(false);                        
                    }
                    removeDeadEnemyFromScene();
                    currentGameState = GameState.TURN_PROCEEDING;
                }
                break;
        }
    }
}
