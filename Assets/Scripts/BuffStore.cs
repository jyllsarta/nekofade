using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStore {

    public static Buff getBuffByBuffID(Buff.BuffID buffID)
    {
        switch (buffID)
        {
            case Buff.BuffID.BLANK:
                return new Buff(Buff.BuffID.BLANK, 10000);
                break; //到達できないコードパスの緑線出るけどswitchでbreak書かないの嫌すぎ
            case Buff.BuffID.GUARD:
                return new Buff(Buff.BuffID.GUARD, 100);
                break;
            case Buff.BuffID.POISON:
                return new Buff(Buff.BuffID.POISON, 500);
                break;
            default:
                Debug.LogWarning("getBuffByBuffIDのデフォルトが呼ばれてる");
                return new Buff(Buff.BuffID.BLANK, 10000);
                break;
        }
    }
}
