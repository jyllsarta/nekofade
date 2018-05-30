using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class MapPoint : MonoBehaviour , IPointerClickHandler{

    public SpriteRenderer image;
    public SpriteRenderer floor;
    public Transform pos;
    public bool isMoveAbailable;
    public Map map;
    public SpriteRenderer siroko;
    public MapEvent mapEvent;

    public bool isDistanceLessThan(Transform target, float distance)
    {
        float d = Vector2.Distance(pos.position, target.position);
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
            floor.color = new Color(0.3f, 0.3f, 0.3f, 0.8f);
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Vector2.Distance(eventData.pressPosition, eventData.position) > 10f)
        {
            Debug.Log("動いとるやんけ");
            return;
        }
        this.onClick();
    }
}
