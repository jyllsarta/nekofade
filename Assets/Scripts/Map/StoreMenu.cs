using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : UIMenu {

    //左右に置くぶんのコンポーネントもう掴んじゃう
    //TODO Instanciateして[アクション+なんか] 以外にも対応できるようにする?
    public StoreAreaComponent leftHand;
    public StoreAreaComponent rightHand;


    // Use this for initialization
    void Start () {
        leftHand.setShopLineup(new List<string>() { "響火", "瞬突", "凍結" }, StoreAreaComponent.ItemKind.ACTION);
        rightHand.setShopLineup(new List<string>() { "ウィンドブーツ", "闇" }, StoreAreaComponent.ItemKind.EQUIP);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
