using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventTreasure : MapEvent, IUIMenuAction
{
    SirokoStats status;
    Map map;
    DialogMenu dialog;
    public enum TreasureType
    {
        ITEM,
        EQUIP,
        ACTION,
    }

    public TreasureType treasureType;
    public string treasureName;

    public MapEventTreasure(SirokoStats status, Map map, TreasureType treasureType, string treasureName, DialogMenu dialog)
    {
        this.status = status;
        this.map = map;
        this.eventType = EventType.TREASURE;
        this.treasureType = treasureType;
        this.treasureName = treasureName;
        this.dialog = dialog;
    }

    public override void startEvent()
    {
        Debug.Log("宝箱！");
        dialog.setTitle("Treasure!");
        switch (treasureType)
        {
            case TreasureType.ACTION:
                dialog.setText(string.Format("{0}の書を拾った！\nアクション{0}を習得した！",treasureName));
                break;
            case TreasureType.EQUIP:
                dialog.setText(string.Format("装備 {0} を拾った！", treasureName));
                break;
            case TreasureType.ITEM:
                dialog.setText(string.Format("アイテム {0} を拾った！", treasureName));
                break;
        }
        dialog.closeHandler = this;
        dialog.show();
    }

    public void OnClose()
    {
        switch (treasureType)
        {
            case TreasureType.ACTION:
                status.addAction(treasureName);
                break;
            case TreasureType.EQUIP:
                status.addEquip(treasureName);
                break;
            case TreasureType.ITEM:
                status.addItem(treasureName);
                map.refreshStatusArea();
                break;
        }
    }
}
