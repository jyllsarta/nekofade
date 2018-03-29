using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEventEnemy : MapEvent {

    List<string> enemies;

    public MapEventEnemy(List<string> enemies)
    {
        this.enemies = enemies;
    }

    public override void startEvent()
    {
        Debug.Log("バトル開始！");
        //SceneManager.LoadScene("battleAlpha");
    }
}
