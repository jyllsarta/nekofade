using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapEvent{
    
    public enum EventType
    {
        EMPTY,
        ENEMY,
        GOLD,
        STORE,
        TREASURE,
        GOAL,
    }

    public EventType eventType;

    public virtual void startEvent()
    {
    } 


}
