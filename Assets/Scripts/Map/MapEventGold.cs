using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventGold : MapEvent {
    int amount;
    SirokoStats status;

    public MapEventGold(int amount, SirokoStats status)
    {
        this.amount = amount;
        this.status = status;
        this.eventType = EventType.GOLD;
    }

    public override void startEvent()
    {
        Debug.Log("おかね！");
        status.gold += amount;
    }

}
