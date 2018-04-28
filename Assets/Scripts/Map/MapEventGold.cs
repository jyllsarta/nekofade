using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapEventGold : MapEvent {
    int amount;
    SirokoStats status;
    Map map;

    public MapEventGold(int amount, SirokoStats status, Map map)
    {
        this.amount = amount;
        this.status = status;
        this.eventType = EventType.GOLD;
        this.map = map;
    }

    public override void startEvent()
    {
        Debug.Log("おかね！");
        status.gold += amount;
        map.playGetGoldEffect(amount);
    }


    
}
