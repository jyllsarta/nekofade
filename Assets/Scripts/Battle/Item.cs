using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{
    public string itemName;
    public int count;
    public bool isPassiveItem;
    public string description;
    public string imagePath;
    public Action action;
    public Rarity rarity;

    public enum Rarity
    {
        COMMON,
        RARE,
        EPIC,
        LEGENDARY,
    }

    public static int getCostByRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.COMMON:
                return 200;
            case Rarity.RARE:
                return 300;
            case Rarity.EPIC:
                return 500;
            case Rarity.LEGENDARY:
                return 800;
            default:
                Debug.LogWarning("getCostByRarityのdefaultが呼ばれてる");
                return 999;
        }
    }

    public Item(string itemName, int count, bool isPassiveItem, string description, string imagePath,Action action, Rarity rarity)
    {
        this.itemName = itemName;
        this.count = count;
        this.isPassiveItem = isPassiveItem;
        this.description = description;
        this.imagePath = imagePath;
        this.action = action;
        this.rarity = rarity;
    }
    public Item()
    {
        Debug.LogWarning("デフォルトコンストラクタ呼ばれてんぞ");
        this.itemName = "かりあいてむ";
        this.count = 1;
        this.isPassiveItem = false;
        this.description = "謎の仮アイテム";
        this.imagePath = "UI_Icon_Warning";
        this.action = new Action();
        this.rarity = Rarity.COMMON;
    }
}
