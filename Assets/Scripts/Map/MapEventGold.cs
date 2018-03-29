using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventGold : MapEvent {
    int amount;

    public MapEventGold(int amount)
    {
        this.amount = amount;
    }

    public override void startEvent()
    {
        Debug.Log("おかね！");
    }

}
