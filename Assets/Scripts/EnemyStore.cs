﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStore : MonoBehaviour{


    public BattleCharacter prefab;

    public BattleCharacter getEnemyByName(string enemyName)
    {
        BattleCharacter createdChild;
        createdChild = Instantiate(prefab);

        switch (enemyName)
        {
            case "scp":
                createdChild.actions.Add("刺突");
                return createdChild;
            default:
                Debug.LogWarning("EnemyName素通りしました");
                return createdChild;
        }

    }
}
