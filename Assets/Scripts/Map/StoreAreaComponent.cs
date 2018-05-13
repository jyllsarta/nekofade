using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreAreaComponent : MonoBehaviour {

    public enum ItemKind
    {
        ACTION,
        ITEM,
        EQUIP,

    }

    //このエリアが何を買うエリアなのか
    public ItemKind kind;

    public List<StoreItem> lineups;

    public void refresh()
    {
        foreach (StoreItem i in lineups)
        {
            i.refresh();
        }
    }

    public void setShopLineup(List<string> itemNames, ItemKind kind)
    {
        Debug.Log("ラインアップ更新");
        for (int i = 0; i < lineups.Count; ++i)
        {
            if (i < itemNames.Count)
            {
                lineups[i].setParameters(itemNames[i], 100, kind); //TODO 価格制御
            }
            else
            {
                lineups[i].setParameters("-", 999, kind);
            }
        }
    }

    public void Start()
    {
        refresh();
    }

}
