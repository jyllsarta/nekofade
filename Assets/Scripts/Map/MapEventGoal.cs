using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEventGoal : MapEvent, IUIMenuAction
{

    public Map map;
    public DialogMenu dialog;

    public MapEventGoal(Map map, DialogMenu dialog)
    {
        this.map = map;
        this.dialog = dialog;
        this.eventType = EventType.GOAL;
    }

    public override void startEvent()
    {
        dialog.show();
        dialog.setTitle("win!");
        dialog.setText("魔王に勝利した！やったー！");
        dialog.closeHandler = this;
    }

    public void OnClose()
    {
        SceneManager.LoadSceneAsync("title");
    }

}
