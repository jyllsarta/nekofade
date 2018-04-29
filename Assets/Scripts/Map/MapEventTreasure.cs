using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventTreasure : MapEvent, IUIMenuAction
{
    SirokoStats status;
    Map map;
    public enum TreasureType
    {
        ITEM,
        EQUIP,
        ACTION,
    }

    public TreasureType treasureType;
    public string treasureName;

    public MapEventTreasure(SirokoStats status, Map map, TreasureType treasureType, string treasureName)
    {
        this.status = status;
        this.map = map;
        this.eventType = EventType.TREASURE;
        this.treasureType = treasureType;
        this.treasureName = treasureName;
    }

    public override void startEvent()
    {
        Debug.Log("宝箱！");
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

    public void OnClose()
    {

    }
}
