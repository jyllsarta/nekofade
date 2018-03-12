using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//アクション一覧
public class BattleActionsArea : MonoBehaviour{

    public ActionButton actionButtonPrefab;
    public BattleCharacter siroko;
    public GameObject contents;
    public Timeline timeline;
    public ScrollRect scroll;
    public MessageArea messageArea;
    
    public void addAction(string actionName)
    {
        ActionButton created = Instantiate(actionButtonPrefab);
        Action a = ActionStore.getActionByName(actionName,siroko);
        created.actionName.text = actionName;
        created.mp.text = a.cost.ToString();
        created.wt.text = a.waitTime.ToString();
        UnityAction<Button, string> addButtonClickEvent = (Button b, string str) =>
        {
            b.onClick.AddListener(() =>
            {
                timeline.tryAddAction(ActionStore.getActionByName(str, siroko));
            });
        };
        addButtonClickEvent(created.button,actionName);
        created.messageArea = messageArea;
        created.transform.SetParent(contents.transform);
    }


    //引数のアクションを読み込み
    public void loadActions(List<string> actions)
    {
        foreach (string actionName in actions)
        {
            addAction(actionName);
        }
        scroll.horizontalNormalizedPosition = 0f;
    }

    // Use this for initialization
    void Start () {
        //読み込み順バグを避けるためにBattleSceneConstructor側でloadactionsを呼ぶことにしました
        //loadActions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
