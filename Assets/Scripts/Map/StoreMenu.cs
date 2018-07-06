using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : UIMenu {

    //左右に置くぶんのコンポーネントもう掴んじゃう
    //TODO Instanciateして[アクション+なんか] 以外にも対応できるようにする?
    public StoreAreaComponent leftHand;
    public StoreAreaComponent rightHand;
    public NumeratableText goldText;
    public SirokoStats status;

    public void refresh()
    {
        if (!status)
        {
            status = FindObjectOfType<SirokoStats>();
        }
        leftHand.refresh();
        rightHand.refresh();
        goldText.numerate(status.gold);
    }

    public new void show()
    {
        gameObject.SetActive(true);
        refresh();
    }

    // Use this for initialization
    void Start () {
        leftHand.setShopLineup(new List<string>() { "高揚", "大鎌", "凍結" }, StoreAreaComponent.ItemKind.ACTION);
        rightHand.setShopLineup(new List<string>() { "さんかく帽子", "霊気の鎧", "闇" }, StoreAreaComponent.ItemKind.EQUIP);
        status = FindObjectOfType<SirokoStats>();
        goldText.numerate(status.gold);
        if (!status)
        {
            Debug.LogError("ステータス掴みそこねた(ストアメニュー)");
        }
        refresh();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
