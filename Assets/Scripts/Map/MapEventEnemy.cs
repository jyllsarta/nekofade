﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapEventEnemy : MapEvent {

    List<string> enemies;
    SirokoStats status;
    Map map;

    public MapEventEnemy(List<string> enemies, Map map, SirokoStats status)
    {
        this.enemies = enemies;
        this.status = status;
        this.eventType = EventType.ENEMY;
        this.map = map;
    }

    public override void startEvent()
    {
        Debug.Log("バトル開始！");
        status.enemies = this.enemies;
        SceneManager.LoadSceneAsync("battleAlpha", LoadSceneMode.Additive);
    }
}
