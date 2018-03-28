using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEvent : MonoBehaviour {
    
    public enum EventType
    {
        EMPTY,
        ENEMY,
        GOLD,
        STORE,
    }

    public EventType eventType;

    public Image image;

    public virtual void startEvent() { }
    public virtual void setEventType() { }

    public void setImage(string resourcePath)
    {
        string pathHeader = "Enemy/";
        string path = pathHeader + resourcePath;
        Sprite s = Resources.Load<Sprite>(path);
        image.sprite = s;
        image.color = new Color(1, 1, 1, 1);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
