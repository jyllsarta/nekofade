using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public Battle battleModel;
    public Timeline timeline;

    public void onButtonClick(string button_id)
    {
        Action a = ActionStore.getActionByName(button_id);
        if (timeline.canAddThis(a))
        {
            timeline.Add(a);
        }
    }

}
