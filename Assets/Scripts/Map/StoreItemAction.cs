using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemAction : StoreItem {

    public ActionButton  actionButton;

    //二箇所にあるの注意 => BattleActionsArea
    Color getRarityColor(Action.Rarity rarity)
    {
        switch (rarity)
        {
            case Action.Rarity.COMMON:
                return new Color(33 / 256f, 37 / 256f, 51 / 256f);
            case Action.Rarity.RARE:
                return new Color(25 / 256f, 35 / 256f, 103 / 256f);
            case Action.Rarity.EPIC:
                return new Color(77 / 256f, 15 / 256f, 81 / 256f);
            case Action.Rarity.LEGENDARY:
                return new Color(92 / 256f, 67 / 256f, 22 / 256f);
            default:
                Debug.Log("getRarityColorのデフォルトが呼ばれてる");
                return new Color(0, 0, 0);
        }
    }

    public override void setParameters(string itemName, int cost, StoreAreaComponent.ItemKind kind)
    {
        Action a = ActionStore.getActionByName(itemName);
        actionButton.actionName.text = itemName;
        actionButton.mp.text = a.cost.ToString();
        actionButton.wt.text = a.waitTime.ToString();
        actionButton.backgroundImage.color = getRarityColor(a.rarity);
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
