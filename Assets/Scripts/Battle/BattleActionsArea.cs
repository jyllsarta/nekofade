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
    public List<ActionButton> actionbuttons;

    public Sprite spear;
    public Sprite rod;
    
    public void addAction(string actionName)
    {
        ActionButton created = Instantiate(actionButtonPrefab,contents.transform);
        Action a = ActionStore.getActionByName(actionName,siroko);
        created.actionName.text = actionName;
        created.mp.text = a.cost.ToString();
        created.wt.text = a.waitTime.ToString();
        if (a.effectList.Exists(x=>x.hasAttribute(Effect.Attribute.MAGIC)))
        {
            created.actionTypeImage.sprite = rod;
        }
        else
        {
            created.actionTypeImage.sprite = spear;
        }
        UnityAction<Button, string> addButtonClickEvent = (Button b, string str) =>
        {
            b.onClick.AddListener(() =>
            {
                timeline.tryAddAction(ActionStore.getActionByName(str, siroko));
            });
        };
        addButtonClickEvent(created.button,actionName);
        created.messageArea = messageArea;

        actionbuttons.Add(created);
    }

    //WTを現在の速度に応じたものに更新
    public void updateActionWaitTime()
    {
        foreach (ActionButton button in actionbuttons)
        {
            int wt = ActionStore.getActionByName(button.actionName.text, siroko).waitTime;
            button.wt.text = wt.ToString();
        }
    }

    //引数のアクションを読み込み
    public void loadActions(List<string> actions)
    {
        actionbuttons = new List<ActionButton>();
        foreach (string actionName in actions)
        {
            addAction(actionName);
        }
        scroll.horizontalNormalizedPosition = 0f;
    }

    //選べないアクションはdisabledつける
    public void updateState()
    {
        foreach (ActionButton a in actionbuttons)
        {
            //getActionByNameが重いとかgetRemainingFramesが重いとかあるならtimeline側で更新を厳密に制御
            if (ActionStore.getActionByName(a.actionName.text, siroko).waitTime <= timeline.getRemainingFrames())
            {
                a.button.interactable = true;
            }
            else
            {
                a.button.interactable = false;
            }

        }
    }

    // Use this for initialization
    void Start () {
        //読み込み順バグを避けるためにBattleSceneConstructor側でloadactionsを呼ぶことにしました
        //loadActions();
	}
	
	// Update is called once per frame
	void Update () {
        updateState();
	}
}
