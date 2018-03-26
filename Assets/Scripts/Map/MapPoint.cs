using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPoint : MonoBehaviour {

    public Image image;
    public RectTransform pos;
    public bool isMoveAbailable;
    public Map map;

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
            image.color = new Color(0.8f, 0.8f, 1f, 1f);
        }
        else
        {
            this.isMoveAbailable = false;
            image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    public void setCurrentPositionToThis()
    {
        map.currentPoint = this;
    }

    public void onClick()
    {
        if (isMoveAbailable)
        {
            setCurrentPositionToThis();
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
}
