using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject pointsContainer;
    public List<MapPoint> points;
    public MapPoint currentPoint;
    public float distance;
    public MapSirokoIllust sirokoillust;

    //Sceneの座標情報を読み込む
    public void loadGeometry()
    {
        points = new List<MapPoint>();
        MapPoint[] scenePoints = pointsContainer.GetComponentsInChildren<MapPoint>();
        foreach (MapPoint p in scenePoints)
        {
            points.Add(p);
        }
    }

    public void setCurrentPoint(MapPoint p)
    {
        currentPoint = p;
        sirokoillust.destination = p;
    }

    void updateAvailablePoints()
    {
        foreach(MapPoint p  in points)
        {
            if (p.isDistanceLessThan(currentPoint.pos, distance))
            {
                p.setMoveAvailableState(true);
            }
            else
            {
                p.setMoveAvailableState(false);
            }
        }
    }

	// Use this for initialization
	void Start () {
        loadGeometry();
	}
	
	// Update is called once per frame
	void Update () {
        updateAvailablePoints();
	}
}
