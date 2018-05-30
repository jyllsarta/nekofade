using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreAreaComponentAction : StoreAreaComponent {

    public void notifyActionIsBoughtToChildren()
    {
        foreach(StoreItemAction storeItem in lineups) //このダウンキャストエラーにならないんだ！？
        {
            storeItem.setBought();
            storeItem.refresh();
        }
    }
}
