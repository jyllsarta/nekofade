using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStore : MonoBehaviour {

    public Buff prefab;
    Buff createdChild;
    public Buff instanciateBuffByBuffID(Buff.BuffID buffID, Transform parent)
    {
        switch (buffID)
        {
            case Buff.BuffID.BLANK:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.BLANK;
                createdChild.setImage("UI_Icon_Warning");
                createdChild.duplicates = false;
                return createdChild;
                //到達できないコードパスの緑線出るけどswitchでbreak書かないの嫌すぎ
            case Buff.BuffID.GUARD:
                Debug.LogWarning("GUARDは呼ばれないはず");
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.GUARD;
                createdChild.setImage("UI_Icon_Defend");
                createdChild.duplicates = true;
                return createdChild;
            case Buff.BuffID.POISON:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 300;
                createdChild.buffID = Buff.BuffID.POISON;
                createdChild.setImage("UI_Icon_Skull");
                createdChild.duplicates = true;
                return createdChild;
            case Buff.BuffID.REGENERATE:
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 180;
                createdChild.buffID = Buff.BuffID.REGENERATE;
                createdChild.setImage("UI_Icon_HeartEmpty");
                createdChild.duplicates = false;
                return createdChild;
            default:
                Debug.LogWarning("getBuffByBuffIDのデフォルトが呼ばれてる");
                createdChild = Instantiate(prefab, parent);
                createdChild.length = 999;
                createdChild.buffID = Buff.BuffID.BLANK;
                createdChild.setImage("UI_Icon_Warning");
                createdChild.duplicates = false;
                return createdChild;
        }
    }
}
