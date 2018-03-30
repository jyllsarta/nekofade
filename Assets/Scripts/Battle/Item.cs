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
    public Item(string itemName, int count, bool isPassiveItem, string description, string imagePath,Action action)
    {
        this.itemName = itemName;
        this.count = count;
        this.isPassiveItem = isPassiveItem;
        this.description = description;
        this.imagePath = imagePath;
        this.action = action;
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
    }
}
