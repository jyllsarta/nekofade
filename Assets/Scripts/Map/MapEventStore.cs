using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapEventStore : MapEvent, IUIMenuAction
{
    StoreMenu store;
    DialogMenu dialog;

    public MapEventStore(Map map, StoreMenu storeMenu, DialogMenu dialog)
    {
        this.eventType = EventType.STORE;
        this.store = storeMenu;
        this.dialog = dialog;
    }

    public override void startEvent()
    {
        dialog.setTitle("Treasure!");
        dialog.setText(string.Format("ピリカちゃんがこっそり開いているお店に来た！"));
        dialog.closeHandler = this;
        dialog.show();
    }

    public void OnClose()
    {
        store.show();
    }

}
