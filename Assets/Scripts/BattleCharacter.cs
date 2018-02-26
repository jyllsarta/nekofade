using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//敵味方共通の処理
public class BattleCharacter : MonoBehaviour {

    public int hp;
    public int maxHp;
    //技一覧

    public bool isDead()
    {
        return hp <= 0;
    }

}
