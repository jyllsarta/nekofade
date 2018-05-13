using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreItemDefault : StoreItem,  IPointerEnterHandler, IPointerExitHandler{
    public TextMeshProUGUI itemNameText;
    public Image background;

    void setText(string s)
    {
        itemName = s;
        itemNameText.text = s;
    }

    public override void setParameters(string itemName, int cost, StoreAreaComponent.ItemKind kind)
    {
        setText(itemName);
        setCost(cost);
        setKind(kind);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        background.color = new Color(97.0f / 256.0f, 97.0f / 256.0f, 119.0f / 256.0f, 183.0f / 256.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        syncSellAvailableState(canBuyThis());
    }

    public override void syncSellAvailableState(bool state)
    {
        button.interactable = state;
        if (state)
        {
            background.color = new Color(57.0f/256.0f, 57.0f / 256.0f, 79.0f / 256.0f, 163.0f / 256.0f);
        }
        else
        {
            background.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }


}
