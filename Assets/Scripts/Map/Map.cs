using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject pointsContainer;
    public List<MapPoint> points;
    public MapPoint currentPoint;
    public float distance;
    public MapSirokoIllust sirokoillust;

    public MapEventEnemy enemyEventPrefab;

    public SirokoStats status;
    public SirokoStats status_default;

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


    public void processEvent(MapEvent e)
    {
        e.startEvent();
    }

    public void setCurrentPoint(MapPoint p)
    {
        currentPoint = p;
        sirokoillust.destination = p;
        processEvent(p.mapEvent);
    }

    void updateAvailablePoints()
    {
        foreach (MapPoint p in points)
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

    public MapPoint findEmptyMapPoint()
    {
        //マップで空のものを選択
        List<MapPoint> p = points.FindAll(x => x.mapEvent.eventType == MapEvent.EventType.EMPTY);
        //そこから自分がいるところを削除
        p.Remove(currentPoint);

        //残りの場所からランダムな点を返す
        return p[Random.Range(0, p.Count)];

    }

    //しろこのいないとこに適当に敵を撒く
    public void putEnemies()
    {
        //TODO 敵をきまりにしたがって置く
        MapPoint p = findEmptyMapPoint();

        p.mapEvent = new MapEventEnemy(new List<string>() { "カニ" });
        p.setImage("Enemy/kani");

    }

    public void loadStatusData()
    {
        SirokoStats s = FindObjectOfType<SirokoStats>();
        if (s == null)
        {
            status = Instantiate(status_default);
            DontDestroyOnLoad(s);
        }
        else
        {
            status = s;
        }
    }

    // Use this for initialization
    void Start()
    {
        loadStatusData();
        loadGeometry();
        putEnemies();

    }

    // Update is called once per frame
    void Update()
    {
        updateAvailablePoints();
    }
}
