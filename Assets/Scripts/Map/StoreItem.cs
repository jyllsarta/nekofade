﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//お店に売ってるやつ(overrideされてます)
public class StoreItem : MonoBehaviour {

    public Button button;
    public TextMeshProUGUI costText;
    public string itemName;
    public int cost;
    public StoreAreaComponent.ItemKind kind;
    public bool isBought;
    public StoreMenu storeMenu;
    public UISFXController soundsController;

    void setText(string s)
    {
        itemName = s;
    }
    protected void setCost(int c)
    {
        cost = c;
        costText.text = c.ToString();
    }
    protected void setKind(StoreAreaComponent.ItemKind kind)
    {
        this.kind = kind;
    }
    public virtual void setParameters(string itemName, int cost, StoreAreaComponent.ItemKind kind)
    {
        setText(itemName);
        setCost(cost);
        setKind(kind);
    }


    public virtual void syncSellAvailableState(bool state)
    {
        button.interactable = state;
    }

    public bool canBuyThis()
    {
        //もう買ってると買えない
        if (isBought)
        {
            return false;
        }

        //お金が足りないと買えない
        SirokoStats status = GameObject.FindObjectOfType<SirokoStats>();
        if (!status)
        {
            Debug.LogError("status掴みそこねた(ストアアイテム)");
        }
        if (status.gold < cost)
        {
            return false;
        }

        return true;
    }

    //もう買ったことにする
    public void setBought()
    {
        isBought = true;
        refresh();
    }

    public void findSoundsController()
    {
        soundsController = FindObjectOfType<UISFXController>();
    }

    public void buy()
    {
        SirokoStats status = FindObjectOfType<SirokoStats>();
        if (!status)
        {
            Debug.LogError("買おうとしたけどステータスない");
            return;
        }
        if (canBuyThis())
        {
            soundsController.playUICoin();
            status.buy(kind, itemName, cost);
            isBought = true;
            refresh();
            storeMenu.refresh();
        }
        else
        {
            Debug.Log("ｶｴﾅｲﾖ");
        }
    }

    public void refresh()
    {
        if (canBuyThis())
        {
            syncSellAvailableState(true);
        }
        else
        {
            syncSellAvailableState(false);
        }

    }

    public void Start()
    {
        isBought = false;
        findSoundsController();
        refresh();
    }
}
