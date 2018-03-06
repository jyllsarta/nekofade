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
    
    public void addAction(string actionName)
    {
        ActionButton created = Instantiate(actionButtonPrefab);
        Action a = ActionStore.getActionByName(actionName);
        created.name.text = actionName;
        created.mp.text = a.cost.ToString();
        created.wt.text = a.waitTime.ToString();
        UnityAction<Button,string> addButtonClickEvent = (Button b, string str) =>
        {
            b.onClick.AddListener(() =>
            {
                timeline.tryAddAction(ActionStore.getActionByName(str));
            });
        };
        addButtonClickEvent(created.button,actionName);
        created.transform.SetParent(contents.transform);
    }


    //自身のフレームにアクション一覧を読み込み
    public void loadActions()
    {
        foreach (string actionName in siroko.actions)
        {
            addAction(actionName);
        }
        scroll.horizontalNormalizedPosition = 0f;
    }

    // Use this for initialization
    void Start () {
        loadActions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
