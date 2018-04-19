using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPoint : MonoBehaviour {

    public Image image;
    public Image floor;
    public RectTransform pos;
    public bool isMoveAbailable;
    public Map map;
    public Image siroko;
    public MapEvent mapEvent;

    public bool isDistanceLessThan(RectTransform target, float distance)
    {
        float d = Vector2.Distance(pos.anchoredPosition, target.anchoredPosition);
        return d < distance;
    }

    public void setMoveAvailableState(bool isAvailable)
    {
        if (isAvailable)
        {
            this.isMoveAbailable = true;
            image.color = new Color(1f, 1f, 1f, 1f);
            floor.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            this.isMoveAbailable = false;
            image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            floor.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    public void setCurrentPositionToThis()
    {
        map.setCurrentPoint(this);
    }

    public void onClick()
    {
        if (isMoveAbailable)
        {
            setCurrentPositionToThis();
        }
    }


    public void setImage(string resourcePath)
    {
        string path = resourcePath;
        Sprite s = Resources.Load<Sprite>(path);
        image.sprite = s;
        image.enabled = true;
    }

    public void startEvent()
    {
        this.mapEvent.startEvent();
        //お店以外のイベントは一度発生したら消える
        if (mapEvent.eventType != MapEvent.EventType.STORE)
        {
            mapEvent = new MapEvent();
            image.enabled = false;
        }
    }


    // Use this for initialization
    void Start () {
        setMoveAvailableState(false);
        map = FindObjectOfType<Map>();
        this.mapEvent = new MapEvent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
