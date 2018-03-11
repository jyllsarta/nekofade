using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timeline : MonoBehaviour{
    public int frameWidth = 1250;
    public List<Action> currentPlayerActions;
    public List<EnemyAction> currentEnemyActions;
    public List<TimelineAction> actionInstances;
    public List<TimelineEnemyAction> enemyActionInstances;
    public int framesPerTurn = 60;

    public TimelineAction commandInstance;
    public TimelineEnemyAction enemyCommandInstance;
    public GameObject commandsView;
    public GameObject enemyCommandsView;
    public GameObject FramePointer;
    public TextMeshProUGUI remainingFrameText;
    public BattleCharacter siroko;

    //現在のゲーム内フレーム
    public int currentFrame;

    public Timeline()
    {
        currentPlayerActions = new List<Action>();
        currentEnemyActions = new List<EnemyAction>();
        currentFrame = 0;
    }

    public void flushEnemyActions()
    {
        //画面に残ったインスタンスを消す
        foreach (TimelineEnemyAction a in enemyActionInstances)
        {
            Destroy(a.gameObject);
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
    public EnemyAction getEnemyActionByFrame(int frame)
    {
        foreach (EnemyAction a in currentEnemyActions)
        {
            if (a.frame == frame)
            {
                return a;
            }
        }
        //なんにもないときにはnull返しちゃう
        return null;
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
        remainingFrameText.text = framesPerTurn.ToString();

        //画面に残ったインスタンスを消す
        foreach(TimelineAction a in actionInstances)
        {
            Destroy(a.gameObject);
        }
        actionInstances.Clear();
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
                Debug.Log(string.Format("{0} / {1}を削除したよ",a.actionName,a.GetHashCode()));
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
    //エフェクト再生中は進まない
    public void proceed()
    {
       currentFrame++;
        updateFramePosition();
    }

    public void Add(Action a)
    {
        //アクションをプレハブから生成
        TimelineAction createdChild = Instantiate(commandInstance);
        //該当アクション用のパラメータに書き換え
        createdChild.transform.parent = commandsView.transform;
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
        int remainingFrames = framesPerTurn - countTotalFrameOfSelectingCommand();
        remainingFrameText.text = remainingFrames.ToString();

        //キャラ側のMP消費(積む前にcanPayThisでMP不足かどうかはチェック済のはず)
        siroko.payCastCost(a);
    }

    public void addEnemyAction(EnemyAction a)
    {        
        //アクションをプレハブから生成
        TimelineEnemyAction createdChild = Instantiate(enemyCommandInstance);
        //該当アクション用のパラメータに書き換え
        createdChild.transform.parent = enemyCommandsView.transform;
        createdChild.transform.localPosition = new Vector3(a.frame * frameWidth / framesPerTurn,0,0);
        createdChild.text.text = a.actionName;

        //現在アクションリストに追加
        currentEnemyActions.Add(a);
        //削除したとき用にインスタンスを握っとく
        enemyActionInstances.Add(createdChild);

        //そいつの削除時に伝えてもらうために自身への参照をこどもに渡す
        createdChild.timeline = this;
        //アクションのハッシュコードを得ておいて削除時にこれだって伝える
        createdChild.hashCode = a.GetHashCode();
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


    public void Update()
    {

    }

}
