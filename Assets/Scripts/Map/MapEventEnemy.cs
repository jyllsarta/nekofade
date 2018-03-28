using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventEnemy : MapEvent {

    //どんな敵がいるかとかがここに書かれる

    public override void startEvent()
    {
        Debug.Log("敵との戦いがここで起こる");
    }

    public override void setEventType()
    {
        setImage("kani");
        eventType = EventType.ENEMY;
    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
