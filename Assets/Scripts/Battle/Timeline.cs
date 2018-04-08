using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timeline : MonoBehaviour{
    public int frameWidth = 1250;
    public List<Action> currentPlayerActions;
    public List<EnemyAction> currentEnemyActions;
    public List<TimelineAction> actionInstances;
    public List<TimelineEnemyAction> enemyActionInstances;
    public int framesPerTurn = 60;
    public int totalFrame = 0;

    public TimelineAction commandInstance;
    public TimelineEnemyAction enemyCommandInstance;
    public RectTransform commandsView;
    public RectTransform enemyCommandsView;
    public GameObject FramePointer;
    public NumeratableText remainingFrameText;
    public BattleCharacter siroko;
    public BattleActionsArea actionsArea;
    public Button playerThinkFinishButton;

    public MessageArea messageArea;

    //現在のゲーム内フレーム
    public int currentFrame;

    public Timeline()
    {
        currentPlayerActions = new List<Action>();
        currentEnemyActions = new List<EnemyAction>();
        currentFrame = 0;
        totalFrame = 0;
    }

    public void flushEnemyActions()
    {
        //画面に残ったインスタンスを消す
        foreach (TimelineEnemyAction a in enemyActionInstances)
        {
            a.playDeleteAnimationAndRemoveFromScene();
        }
        enemyActionInstances.Clear();
        currentEnemyActions.Clear();
    }

    //frame時刻になんかアクションある?
    public Action getActionByFrame(int frame)
    {
        int sum = 0;

        foreach (Action a in currentPlayerActions)
        {
            sum += a.waitTime;
            if (sum == frame)
            {
                return a;
            }
        }
        //なんにもないときにはnull返しちゃう
        return null;
    }

    //frame時刻になんかアクションある?
    public List<EnemyAction> getEnemyActionByFrame(int frame)
    {
        List<EnemyAction> ea = new List<EnemyAction>();
        foreach (EnemyAction a in currentEnemyActions)
        {
            if (a.frame == frame)
            {
                ea.Add(a);
            }
        }
        return ea;
    }

    //この戦闘で経過した総フレーム数
    public int getTotalFrame()
    {
        return totalFrame;
    }

    //アクションリストのリセット
    public void flushActions(bool returnsMP=true)
    {
        //MPの払い戻し
        if (returnsMP)
        {
            foreach (Action a in currentPlayerActions)
            {
                siroko.returnCastCost(a);
            }
        }
        currentPlayerActions = new List<Action>();
        remainingFrameText.set(framesPerTurn);

        //画面に残ったインスタンスを消す
        foreach(TimelineAction a in actionInstances)
        {
            Destroy(a.gameObject);
        }
        actionInstances.Clear();
        updatePlayerThinkFinishButton();
    }

    public bool isPlayerSpellAriaing()
    {
        int sum = 0;
        //次のスペルはどれ？
        foreach (Action a in currentPlayerActions)
        {
            if (sum + a.waitTime >= currentFrame)
            {
                if (a.isMagic())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                sum += a.waitTime;
            }
        }
        return false;
    }

    //このハッシュを持つキャラの行動をキャンセル
    public void removeEnemyActionByHash(int hashCode)
    {
        //画面に残ったインスタンスを消す
        foreach (TimelineEnemyAction a in enemyActionInstances)
        {
            if (a.actorHash == hashCode)
            {
                a.playDeleteAnimationAndRemoveFromScene();
            }
        }
        enemyActionInstances.RemoveAll(x => x.actorHash == hashCode);
        currentEnemyActions.RemoveAll(x => x.actorHash == hashCode);
    }
    //このハッシュのアクションをキャンセル
    public void removeEnemyActionByActionHash(int hashCode)
    {
        //画面に残ったインスタンスを消す
        foreach (TimelineEnemyAction a in enemyActionInstances)
        {
            if (a.hashCode == hashCode)
            {
                a.playDeleteAnimationAndRemoveFromScene();
            }
        }
        enemyActionInstances.RemoveAll(x => x.hashCode == hashCode);
        currentEnemyActions.RemoveAll(x => x.GetHashCode() == hashCode);
    }

    //現在のコマンド状況から次に積むコマンドの設置場所を計算
    public Vector3 getNextActionPositionStart()
    {
        float frames = countTotalFrameOfSelectingCommand();
        float position = frames / framesPerTurn * frameWidth;
        return new Vector3(position, 0, 0);
    }

    //このアクションが画面上でどれだけ横幅を取るか
    public float getWidthOfAction(Action a)
    {
        float width = (float)a.waitTime / framesPerTurn * frameWidth;
        return width;
    }

    //指定したhashCodeのアクションを削除
    public void removeAction(int hashCode)
    {
        //一旦全部消しちゃって
        //ハッシュコードの一致しないやつ = 削除してないやつだけでアクションを配置しなおす
        //listから消すだけだと横幅いじったり何だりで事故りそうなので
        //Instantiateのコストがきつくなければこのままでいこう
        List<Action> reserved = currentPlayerActions;
        flushActions();
        foreach (Action a in reserved)
        {
            if (hashCode != a.GetHashCode())
            {
                Add(a);
            }
            else
            {
                //Debug.Log(string.Format("{0} / {1}を削除したよ",a.actionName,a.GetHashCode()));
            }
        }
    }

    //新しいターンにする
    public void newTurn()
    {
        currentFrame = 0;
        flushActions(false);
        flushEnemyActions();
        updateFramePosition();
    }

    public void updateFramePosition()
    {
        float x = (float)currentFrame * frameWidth / framesPerTurn;
        FramePointer.transform.localPosition = new Vector3(x, 0, 0);
    }

 

    //フレームを1すすめる
    public void proceed()
    {
        currentFrame++;
        totalFrame++;
        updateFramePosition();
    }

    public void Add(Action a)
    {
        //アクションをプレハブから生成
        TimelineAction createdChild = Instantiate(commandInstance,commandsView.transform);
        //該当アクション用のパラメータに書き換え
        createdChild.transform.localPosition = getNextActionPositionStart();
        createdChild.text.text = a.actionName;

        //アクションの横幅反映
        float y = createdChild.frameImage.sizeDelta.y;
        float x = getWidthOfAction(a);
        createdChild.frameImage.sizeDelta = new Vector2(x,y);

        //現在アクションリストに追加
        currentPlayerActions.Add(a);
        //削除したとき用にインスタンスを握っとく
        actionInstances.Add(createdChild);

        //そいつの削除時に伝えてもらうために自身への参照をこどもに渡す
        createdChild.timeline = this;
        //アクションのハッシュコードを得ておいて削除時にこれだって伝える
        createdChild.hashCode = a.GetHashCode();

        //UI反映
        int remainingFrames = getRemainingFrames();
        remainingFrameText.numerate(remainingFrames);

        //キャラ側のMP消費(積む前にcanPayThisでMP不足かどうかはチェック済のはず)
        siroko.payCastCost(a);
        updatePlayerThinkFinishButton();

    }

    public int getRemainingFrames()
    {
        return framesPerTurn - countTotalFrameOfSelectingCommand();
    }

    public void updatePlayerThinkFinishButton()
    {
        //この追加でアクションが積めるかどうか変わったはずなので変更を反映
        if (isActionsAreaFull())
        {
            //Debug.Log("いっか");
            playerThinkFinishButton.image.color = new Color(1, 1, 0, 1);
        }
        else
        {
            //Debug.Log("まだ");
            playerThinkFinishButton.image.color = new Color(142f / 256, 178f / 256, 221f / 256, 1);
        }
    }

    //アクションがこれ以上積めないかどうか
    public bool isActionsAreaFull()
    {
        return siroko.actions.TrueForAll(x => !canAddThis(ActionStore.getActionByName(x,siroko)));
    }

    //直前/直後のアクションの位置を見てなるべく被らないようにアクションを設置する
    public bool shouldBePlacedToUpperSide(int frame)
    {
        //なんにもアクションがないなら上に置く
        if (currentEnemyActions.Count == 0)
        {
            return true;
        }

        //前か後ろ15Fにアクションがあるなら、一番近いものと逆の位置に置く(O(n^2)のコードなので遅くて気になるなら工夫する)
        for (int i=0;i<12;++i)
        {
            List<EnemyAction> filtered = currentEnemyActions.FindAll(x => x.frame == frame - i || x.frame == frame + i);
            if (filtered.Count > 0)
            {
                return !filtered[0].isUpperSide;
            }
        }

        //近くにないなら上に置く
        return true;
    }

    public void addEnemyAction(EnemyAction a, BattleCharacter enemy)
    {
        //アクションをプレハブから生成
        TimelineEnemyAction createdChild = Instantiate(enemyCommandInstance, enemyCommandsView.transform);
        //該当アクション用のパラメータに書き換え
        createdChild.transform.localPosition = new Vector3(a.frame * frameWidth / framesPerTurn, a.isUpperSide?0:-60 ,0);
        createdChild.actionName.text = a.actionName;
        //画像をそのキャラのものに変更
        createdChild.setImage(enemy.image);

        //詠唱中時間を表すバーを表示
        if (a.effectList.Exists(x=>x.hasAttribute(Effect.Attribute.MAGIC)))
        {
            createdChild.spellCast.sizeDelta = new Vector2(a.waitTime * frameWidth / framesPerTurn, createdChild.spellCast.sizeDelta.y);
        }

        createdChild.predictDamage.text = a.predictedDamage.ToString();

        //現在アクションリストに追加
        currentEnemyActions.Add(a);
        //削除したとき用にインスタンスを握っとく
        enemyActionInstances.Add(createdChild);

        createdChild.messageArea = messageArea;
        createdChild.description = a.descriptionText;

        //そいつの削除時に伝えてもらうために自身への参照をこどもに渡す
        createdChild.timeline = this;
        //アクションのハッシュコードを得ておいて削除時にこれだって伝える
        createdChild.hashCode = a.GetHashCode();
        //そのアクションを取ったキャラのハッシュ(キャラ死亡時に該当キャラのアクションを全部消すため)
        createdChild.actorHash = a.actorHash;
    }

    //現在積まれてるアクションの合計フレーム数を返す
    public int countTotalFrameOfSelectingCommand(){
        int sum = 0;
        foreach (Action a in currentPlayerActions)
        {
            sum += a.waitTime;
        }
        return sum;
    }

    //boolだから戻り値isにしたいけどisAddableはあんまりにもあんまりなのでごまかす
    public bool canAddThis(Action a)
    {
        bool remainingFramesIsSufficient = countTotalFrameOfSelectingCommand() + a.waitTime <= framesPerTurn;
        bool mpIsSufficient = siroko.canPayThis(a);
        return remainingFramesIsSufficient && mpIsSufficient;
    }

    public void tryAddAction(Action a)
    {
        if (canAddThis(a))
        {
            //Debug.Log("いいよー");
            Add(a);
        }
        else
        {
            //Debug.Log("だめー");
        }
    }

    public void Start()
    {
        frameWidth = (int)commandsView.rect.width;
    }

    public void Update()
    {

    }

}
