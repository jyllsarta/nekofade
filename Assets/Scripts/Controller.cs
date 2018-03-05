using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public Battle battleModel;
    public GameObject CommandFrame;

    public void onButtonClick(string button_id)
    {
        switch (button_id)
        {
            case "火炎":
                Debug.Log("かえん！");
                battleModel.consumeAction(ActionStore.getActionByName("火炎"),Battle.ActorType.PLAYER);
                break;
            case "刺突":
                Debug.Log("やり！");
                battleModel.consumeAction(ActionStore.getActionByName("刺突"), Battle.ActorType.PLAYER);
                break;
            case "防御":
                Debug.Log("かちーん！");
                battleModel.consumeAction(ActionStore.getActionByName("防御"), Battle.ActorType.PLAYER);
                break;
            case "吸収":
                Debug.Log("きゅうん！");
                battleModel.consumeAction(ActionStore.getActionByName("吸収"), Battle.ActorType.PLAYER);
                break;
            case "雷光":
                Debug.Log("ごーらい！");
                battleModel.consumeAction(ActionStore.getActionByName("雷光"), Battle.ActorType.PLAYER);
                break;
            case "毒霧":
                Debug.Log("どくどく！");
               　battleModel.consumeAction(ActionStore.getActionByName("毒霧"), Battle.ActorType.PLAYER);
                break;
            default:
                Debug.LogWarning("onButtonClickのデフォルトが呼ばれちゃった！");
                break;
        }
    }

}
