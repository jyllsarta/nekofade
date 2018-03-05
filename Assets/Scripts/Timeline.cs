using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour{
    public int frameWidth = 1600;
    public List<Action> currentActions;
    public int framesPerTurn = 60;

    public TimelineAction commandInstance;
    public GameObject commandsView;

    public Timeline()
    {
        currentActions = new List<Action>();
    }

    public void flushActions()
    {
        currentActions = new List<Action>();
    }

    //現在のコマンド状況から次に積むコマンドの設置場所を計算
    public Vector3 getNextActionPositionStart()
    {
        float frames = countTotalFrameOfSelectingCommand();
        float position = frames / framesPerTurn * frameWidth;
        return new Vector3(position, 0, 0);
    }

    public float getWidthOfAction(Action a)
    {
        float width = (float)a.waitTime / framesPerTurn * frameWidth;
        return width;
    }

    public void Add(Action a)
    {
        TimelineAction createdChild = Instantiate(commandInstance);
        createdChild.transform.parent = commandsView.transform;
        createdChild.transform.localPosition = getNextActionPositionStart();
        createdChild.text.text = a.actionName;
        float y = createdChild.frameImage.sizeDelta.y;
        float x = getWidthOfAction(a);
        createdChild.frameImage.sizeDelta = new Vector2(x,y);
        currentActions.Add(a);
    }

    public int countTotalFrameOfSelectingCommand(){
        int sum = 0;
        foreach (Action a in currentActions)
        {
            sum += a.waitTime;
        }
        return sum;
    }

    //boolだから戻り値isにしたいけどisAddableはあんまりにもあんまりなのでごまかす
    public bool canAddThis(Action a)
    {
        return countTotalFrameOfSelectingCommand() + a.waitTime <= framesPerTurn;
    }


    public void Update()
    {

    }

}
