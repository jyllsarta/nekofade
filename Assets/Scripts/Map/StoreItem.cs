using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//お店に売ってるやつ
public class StoreItem : MonoBehaviour {

    public TextMeshProUGUI itemNameText;
    public Button button;
    public TextMeshProUGUI costText;
    public string itemName;
    public int cost;
    public StoreAreaComponent.ItemKind kind;

    void setText(string s)
    {
        itemName = s;
        itemNameText.text = s;
    }
    void setCost(int c)
    {
        cost = c;
        costText.text = c.ToString();
    }
    void setKind(StoreAreaComponent.ItemKind kind)
    {
        this.kind = kind;
    }
    public void setParameters(string itemName, int cost, StoreAreaComponent.ItemKind kind)
    {
        if (kind == StoreAreaComponent.ItemKind.ACTION)
        {
            Action a = ActionStore.getActionByName(itemName);
            ActionButton button = GetComponent<ActionButton>();
            if (!button)
            {
                Debug.LogError("ActionButtonついてないやんけな");
            }
            button.actionName.text = itemName;
            button.mp.text = a.cost.ToString();
            button.wt.text = a.waitTime.ToString();
            this.itemName = itemName;
            this.cost = cost;
            this.kind = kind;
            //TODO アイコン対応 どころかActionnButtonがじぶんでパラメータ設定したほうが良さそうだ
        }
        else
        {
            setText(itemName);
            setCost(cost);
            setKind(kind);
        }
    }

    public void buy()
    {
        SirokoStats status = FindObjectOfType<SirokoStats>();
        if (!status)
        {
            Debug.LogError("買おうとしたけどステータスない");
            return;
        }
        status.buy(kind, itemName, cost);
    }
}
