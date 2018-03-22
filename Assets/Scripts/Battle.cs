using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Battle : MonoBehaviour {
    public BattleCharacter player;
    public List<BattleCharacter> enemies;
    public List<BattleItem> items;
    public EnemyStore enemyStore;
    public BuffStore buffStore;
    public ItemStore itemStore;
    public Timeline timeline;
    public GameObject enemiesUI;
    public GameObject targetCircle;
    public GameObject itemsContainer;
    public BattleActionsArea actionButtonArea;
    public MessageArea messageArea;

    public DamageEffect damageEffect;
    public DamageEffect healEffect;

    public ActionCutIn actionCutIn;
    public GameObject interruptEffect;

    public EffectSystem effectSystem;

    public TextMeshProUGUI turnCountText;

    //のこりエフェクトリスト
    public LinkedList<PlayableEffect> effectList;

    //あと何フレームエフェクト再生で止まるか
    public int remainingEffectAnimationframes;

    //今ターゲットしてる敵の番号
    public int currentTargettingEnemyIndex;

    //バトルシーンの状態　プレイヤーのコマンド受付なのかバトル中なのか
    public GameState currentGameState;

    //今なんターン目？(timelineとどっちが持つべきだろう)
    public int turnCount;

    public enum GameState
    {
        PLAYER_THINK,
        TURN_PROCEEDING,
        EFFECT_WAITING,
        ITEM_EFFECT_WAITING,
    }

    public enum ActorType
    {
        ENEMY,
        PLAYER,
    }

	// Use this for initialization
	void Start () {
        currentTargettingEnemyIndex = 0;
        turnCount = 0;
        effectList = new LinkedList<PlayableEffect>();
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

    //リストに従ってアイテムを置く
    public void setItems(List<string> itemList)
    {
        //TODO 戦闘をまたぐ場合アイテム個数は消耗していることがある
        items = new List<BattleItem>();
        foreach (string s in itemList)
        {
            BattleItem created = itemStore.instanciateItemByName(s, itemsContainer.transform);
            items.Add(created);
        }

    }

    //アイテム
    public void useItem(BattleItem item)
    {
        consumeActionByItem(item.action, ActorType.PLAYER);
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
        //Debug.LogFormat("{0}!これね",hashCode);

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
        //Debug.Log("敵のハッシュ検索失敗したよ");
    }

    void checkBattleFinish()
    {
        //プレイヤーが死んだら負け
        if (player.isDead())
        {
            if (items.Exists(x=>x.itemName=="復活の御魂"))
            {
                BattleItem item = items.Find(x => x.itemName == "復活の御魂");
                //開放効果はターン1回まで
                if (!item.isUsed)
                {
                    Debug.Log("御魂により復活！");
                    showActionName("御魂の開放", player);
                    player.hp = player.maxHp;
                    item.useItemPassive();
                    return;
                }
            }
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
    int calcDamage(BattleCharacter actor, BattleCharacter target, Effect effect, bool ignoreShield=false, bool isCheck=false)
    {
        //無敵状態だったらダメージは受けない　防御も消費しない
        if (target.hasBuff(Buff.BuffID.INVINCIBLE))
        {
            return 0;
        }

        //消魔印を持っている場合魔法ダメージは1(isCheckがついてる場合予測ダメージ計算でしかないのでスルーする)
        if (target==player && items.Exists(x => x.itemName == "消魔印") && effect.hasAttribute(Effect.Attribute.MAGIC) && !isCheck)
        {
            BattleItem item = items.Find(x => x.itemName == "消魔印");
            //開放効果はターン1回まで
            if (!item.isUsed)
            {
                showActionName("消魔印", player);
                item.useItemPassive();
                return 1;
            }
        }


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


        //物理攻撃で相手が防御してたら防御回数を減らしつつダメージ減衰
        if (!effect.hasAttribute(Effect.Attribute.MAGIC) &&  target.hasShield() && !ignoreShield)
        {
            multiply *= (1f-target.getDefenceCutRate());
            target.shieldCount -= 1;
            //Debug.LogFormat("防御！{0}%ダメージカット", target.getDefenceCutRate()*100);
        }

        //特効枠
        //王撃 5ターン目以降のみつよい
        if (effect.hasAttribute(Effect.Attribute.KING) && turnCount >= 5)
        {
            //Debug.Log("王撃！7倍ダメージ");
            multiply *= 7;
        }
        //ぷち王撃 3ターン目以降のみつよい
        if (effect.hasAttribute(Effect.Attribute.PETIT_KING) && turnCount >= 3)
        {
            //Debug.Log("ぷち王撃！3倍ダメージ");
            multiply *= 3;
        }
        //即撃 1ターン目のみつよい
        if (effect.hasAttribute(Effect.Attribute.SKIP) && turnCount == 1)
        {
            //Debug.Log("即撃！2.5倍ダメージ");
            multiply *= 2.5f;
        }
        //ぷち即撃 2ターン目までつよい
        if (effect.hasAttribute(Effect.Attribute.PETIT_SKIP) && turnCount <= 2)
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

    void resolveHeal(BattleCharacter actor, ref BattleCharacter target, int value, bool isExceed=false)
    {
        if (target.hp < target.maxHp)
        {
            target.hp += value;
            if (target.hp > target.maxHp && !isExceed)
            {
                target.hp = target.maxHp;
            }
        }
        //エフェクトの再生
        DamageEffect createdEffect = Instantiate(healEffect, target.transform);
        createdEffect.damageText.text = value.ToString();
        createdEffect.transform.position = target.transform.position;
    }
    void resolveMpHeal(BattleCharacter actor, ref BattleCharacter target, int value, bool isExceed=false)
    {
        if (target.mp < target.maxMp)
        {
            target.mp += value;
            if (target.mp > target.maxMp && !isExceed)
            {
                target.mp = target.maxMp;
            }
        }
        //エフェクトの再生
        DamageEffect createdEffect = Instantiate(healEffect, target.transform);
        createdEffect.damageText.text = value.ToString();
        createdEffect.transform.position = target.transform.position;
    }

    //敵が攻撃してきた際の予測ダメージ量を返す
    public int getPredictedDamage(BattleCharacter actor, BattleCharacter target, Action action)
    {
        int sum = 0;
        foreach (Effect e in action.effectList)
        {
            //攻撃効果なら
            if (e.targetType == Effect.TargetType.TARGET_ALL || e.targetType == Effect.TargetType.TARGET_SINGLE || e.targetType == Effect.TargetType.TARGET_SINGLE_RANDOM)
            {
                sum += calcDamage(actor, target, e, true,true);
            }
        }
        return sum;
    }

    //該当キャラが魔法を詠唱中で防壁がなかった場合、行動をキャンセルさせる
    void tryInterrptEffect(BattleCharacter target, Effect effect)
    {
        if (target.hasShield() && !effect.hasAttribute(Effect.Attribute.MAGIC))
        {
            //Debug.Log("防壁に弾かれた！");
            return;
        }
        foreach(EnemyAction e in timeline.currentEnemyActions)
        {
            if (e.actorHash != target.GetHashCode())
            {
                //Debug.Log("この人のアクションじゃなかった！");
                continue;
            }
            if (!(e.frame - e.waitTime <= timeline.currentFrame && timeline.currentFrame < e.frame))
            {
                //Debug.Log("詠唱中じゃなかった！");
                continue;
            }
            if (e.effectList.Exists(x => x.hasAttribute(Effect.Attribute.MAGIC)))
            {
                //Debug.Log("魔法だ！これが正解なので消して処理を中断！");
                Instantiate(interruptEffect, timeline.transform);
                timeline.removeEnemyActionByActionHash(e.GetHashCode());
                return;
            }   
            
        }

    }

    void checkEnchantedAttack(BattleCharacter actor, Effect effect)
    {
        //エンチャントファイアによる追加攻撃
        //エンチャントファイア状態を持ってて魔法攻撃でないなら追加攻撃
        //この効果を使うのは自分のみなのでActorTypeにPLAYERを決め撃ちしてる
        if (actor.hasBuff(Buff.BuffID.ENCHANT_FIRE) && !effect.hasAttribute(Effect.Attribute.MAGIC))
        {
            List<Effect.Attribute> attributes = new List<Effect.Attribute>() { Effect.Attribute.MAGIC, Effect.Attribute.FIRE};
            Effect fire = new Effect(Effect.TargetType.TARGET_SINGLE_RANDOM, 20, Effect.EffectType.DAMAGE, 30, attributes);
            PlayableEffect pe = new PlayableEffect(fire, ActorType.PLAYER, 0);
            effectList.AddFirst(pe);
        }
    }

    void tryRemoveShieldEffect(ref BattleCharacter target, Effect effect)
    {
        if (effect.hasAttribute(Effect.Attribute.REMOVE_SHIELD))
        {
            target.shieldCount = 0;
        }
    }


    //実際のEffect一つの処理
    void resolveEffect(BattleCharacter actor, ref BattleCharacter target, Effect effect)
    {
        switch (effect.effectType)
        {
            case Effect.EffectType.DAMAGE:
                tryRemoveShieldEffect(ref target, effect);
                tryInterrptEffect(target, effect);
                int damage = calcDamage(actor, target, effect);
                resolveDamage(actor, ref target, damage);
                effectSystem.playEffectByName("hit", target.transform);
                checkEnchantedAttack(actor, effect);
                break;
            case Effect.EffectType.HEAL:
                int value = calcDamage(actor, target, effect);
                resolveHeal(actor, ref target, value);
                break;
            case Effect.EffectType.CONSTANTHEAL:
                resolveHeal(actor, ref target, effect.effectAmount);
                break;
            case Effect.EffectType.CONSTANTEXCEEDHEAL:
                resolveHeal(actor, ref target, effect.effectAmount, true);
                break;
            case Effect.EffectType.MPHEAL:
                int mpvalue = calcDamage(actor, target, effect);
                resolveMpHeal(actor, ref target, mpvalue);
                break;
            case Effect.EffectType.CONSTANTMPHEAL:
                resolveMpHeal(actor, ref target, effect.effectAmount);
                break;
            case Effect.EffectType.CONSTANTEXCEEDMPHEAL:
                resolveMpHeal(actor, ref target, effect.effectAmount, true);
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

    //アクション名を表示
    public void showActionName(string actionName, BattleCharacter actor)
    {
        ActionCutIn created = Instantiate(actionCutIn, actor.transform);
        created.text.text = actionName + "！";
        //アクション名表示で時間を少し止める
        remainingEffectAnimationframes = 50;
    }

    //どのアクションを、 敵と味方どっちの、何人目が行うかを指定して実際の効果となるエフェクトをキューに積む
    //味方の場合actorIndexは自明に0なので省略可
    public void consumeAction(Action action, ActorType actortype, int actorIndex = 0)
    {
        //アクション名を表示
        if (actortype == ActorType.PLAYER)
        {
            showActionName(action.actionName, player);
        }
        else
        {
            showActionName(action.actionName, enemies[actorIndex]);
        }

        foreach (Effect effect in action.effectList)
        {
            effectList.AddLast(new PlayableEffect(effect, actortype, actorIndex));
        }
        //今積んだエフェクト再生が終わるまでタイムラインは停止する
        currentGameState = GameState.EFFECT_WAITING;
    }

    //どのアクションを、 敵と味方どっちの、何人目が行うかを指定して実際の効果となるエフェクトをキューに積む
    //味方の場合actorIndexは自明に0なので省略可
    public void consumeActionByItem(Action action, ActorType actortype, int actorIndex = 0)
    {
        //アクション名を表示
        if (actortype == ActorType.PLAYER)
        {
            showActionName(action.actionName, player);
        }
        else
        {
            showActionName(action.actionName, enemies[actorIndex]);
        }

        foreach (Effect effect in action.effectList)
        {
            effectList.AddLast(new PlayableEffect(effect, actortype, actorIndex));
        }
        //今積んだエフェクト再生が終わるまでタイムラインは停止する
        currentGameState = GameState.ITEM_EFFECT_WAITING;
    }

    //ターンの初めの処理
    public void onTurnStart()
    {
        turnCount++;
        timeline.newTurn();
        currentGameState = GameState.PLAYER_THINK;
        putEnemyAction();
        player.OnTurnStart();
        foreach (BattleCharacter e in enemies)
        {
            e.OnTurnStart();
        }
        foreach (BattleItem i in items)
        {
            i.setUsed(false);
        }
        actionButtonArea.updateActionWaitTime();
        turnCountText.text = turnCount.ToString();
    }

    public void turnEnd()
    {
        currentGameState = GameState.TURN_PROCEEDING;
        messageArea.updateText("");
        if (player.hasBuff(Buff.BuffID.ADDITIONAL_ATTACK))
        {
            consumeAction(ActionStore.getActionByName("幻閃起動"), ActorType.PLAYER);
        }
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
        if (timeline.currentFrame == timeline.framesPerTurn)
        {
            if (player.hasBuff(Buff.BuffID.ADDITIONAL_ENDATTACK))
            {
                consumeAction(ActionStore.getActionByName("追幻起動"), ActorType.PLAYER);
            }
        }
    }

    void addEnemyAction(BattleCharacter enemy)
    {
        string actionName = enemy.nextAction();
        EnemyAction a = new EnemyAction(ActionStore.getActionByName(actionName, enemy));
        a.actorHash = enemy.GetHashCode();
        a.frame = Random.Range(1, timeline.framesPerTurn);
        a.predictedDamage = getPredictedDamage(enemy, player, a);
        a.isUpperSide = timeline.shouldBePlacedToUpperSide(a.frame);
        timeline.addEnemyAction(a);
    }

    //生きてる敵が自分の行動を積む
    void putEnemyAction()
    {
        if (player.hasBuff(Buff.BuffID.CANSEL_ENEMYFIRSTTURN) && turnCount==1)
        {
            Debug.Log("【先攻奪取】発動！");
            return;
        }
        foreach (BattleCharacter enemy in enemies)
        {
            if (enemy.isDead())
            {
                continue;
            }
            addEnemyAction(enemy);
            //2回行動する敵はもう一回積んでくる
            if (enemy.hasAttribute(CharacterAttribute.AttributeID.ACTIONS_TWICE))
            {
                Debug.Log("二階行動");
                addEnemyAction(enemy);
            }
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

    //エフェクト再生を1フレームぶん進める
    void proceedEffectPlay()
    {
        //再生中ならとりあえずそれが終了するまでエフェクトの再生を続けてもらう
        if (remainingEffectAnimationframes > 0)
        {
            remainingEffectAnimationframes--;
            return;
        }
        //再生終わり、まだエフェクトが残ってるなら次のエフェクトの再生に移る
        else if (effectList.Count > 0)
        {
            PlayableEffect pe = effectList.First.Value;
            effectList.RemoveFirst();
            consumeEffect(pe);
            remainingEffectAnimationframes = pe.blockingFrames;
        }
    }

    // Update is called once per frame
    void Update(){
            checkBattleFinish();
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
                if (timeline.currentFrame >= timeline.framesPerTurn && effectList.Count == 0)
                {
                    triggerEveryCharacterTurnEndEffect();
                    onTurnStart();
                }
                break;
            case GameState.EFFECT_WAITING:
                proceedEffectPlay();
                //再生が終わったら後始末をしてターンに戻る
                if (remainingEffectAnimationframes <= 0 && effectList.Count == 0)
                {
                    //このアクションで敵が死んだ場合にはリターゲット
                    if (enemies[currentTargettingEnemyIndex].isDead())
                    {
                        currentTargettingEnemyIndex = getIndexOfActiveEnemy();
                        targetCircle.SetActive(false);
                    }
                    //キャラが死んだり速度に変化があるかもしれないので反映
                    removeDeadEnemyFromScene();
                    actionButtonArea.updateActionWaitTime();
                    currentGameState = GameState.TURN_PROCEEDING;
                }
                break;
            case GameState.ITEM_EFFECT_WAITING:
                proceedEffectPlay();
                //再生が終わったら行動選択に戻る
                if (remainingEffectAnimationframes <= 0 && effectList.Count == 0)
                {
                    //このアクションで敵が死んだ場合にはリターゲット
                    if (enemies[currentTargettingEnemyIndex].isDead())
                    {
                        currentTargettingEnemyIndex = getIndexOfActiveEnemy();
                        targetCircle.SetActive(false);
                    }
                    //キャラが死んだり速度に変化があるかもしれないので反映
                    removeDeadEnemyFromScene();
                    actionButtonArea.updateActionWaitTime();
                    currentGameState = GameState.PLAYER_THINK;
                }
                break;
        }
    }
}
