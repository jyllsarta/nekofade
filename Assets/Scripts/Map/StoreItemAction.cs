using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemAction : StoreItem {

    public ActionButton  actionButton;

    public override void setParameters(string itemName, int cost, StoreAreaComponent.ItemKind kind)
    {
        Action a = ActionStore.getActionByName(itemName);
        actionButton.actionName.text = itemName;
        actionButton.mp.text = a.cost.ToString();
        actionButton.wt.text = a.waitTime.ToString();
        this.itemName = itemName;
        setCost(Action.getCostByRarity(a.rarity));
        this.kind = kind;
        //TODO アイコン対応 どころかActionnButtonがじぶんでパラメータ設定したほうが良さそうだ
    }

    public override void syncSellAvailableState(bool state)
    {
        button.interactable = state;
        actionButton.button.interactable = state;
    }



}
