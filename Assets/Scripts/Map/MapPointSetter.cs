using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointSetter : MonoBehaviour {

    public Map map;
    public SirokoStats status;

    public MapPoint goalPoint;
    public MapPoint bossPoint;
    public MapPoint treasurePoint_1;
    public MapPoint treasurePoint_2;

    public MapPoint findEmptyMapPoint()
    {
        //マップで空のものを選択
        List<MapPoint> p = map.points.FindAll(x => x.mapEvent.eventType == MapEvent.EventType.EMPTY);
        //そこから自分がいるところを削除
        p.Remove(map.currentPoint);
        //残りの場所からランダムな点を返す
        return p[Random.Range(0, p.Count)];

    }

    public void loadStatusData()
    {
        SirokoStats s = FindObjectOfType<SirokoStats>();
        if (s == null)
        {
            Debug.LogWarning("ステータス掴みそこねた");
        }
        else
        {
            status = s;
        }
    }


    //お金のマス置く
    public void putGolds()
    {
        MapPoint p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(30, status, map);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(5, status, map);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(200, status, map);
        p.setImage("etc/mapgold");
    }


    //しろこのいないとこに適当に敵を撒く
    public void putEnemies()
    {
        bossPoint.mapEvent = new MapEventEnemy(new List<string>() { "魔王" }, status);
        bossPoint.setImage("Enemy/kingNeko");

        goalPoint.mapEvent = new MapEventGoal(map, map.dialog);
        goalPoint.setImage("SimpleVectorIcons/UI_Icon_InputJoystick");

        treasurePoint_1.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.ITEM, "復活の御魂", map.dialog);
        treasurePoint_1.setImage("SimpleVectorIcons/UI_Icon_Bag1");

        treasurePoint_2.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.ACTION, "癒陣", map.dialog);
        treasurePoint_2.setImage("SimpleVectorIcons/UI_Icon_Bag1");

        MapPoint p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "ゴブニキ" }, status);
        p.setImage("Enemy/gob");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "ゴブニキ", "ゴブニキ" }, status);
        p.setImage("Enemy/gob");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "ヤリゴブニキ", "ゴブニキ" }, status);
        p.setImage("Enemy/gob_rance");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "ダスティ" }, status);
        p.setImage("Enemy/dust");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "にゃーさん" }, status);
        p.setImage("Enemy/nya");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "ブルーにゃーさん" }, status);
        p.setImage("Enemy/nya_blue");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "妖精", "ゴブニキ" }, status);
        p.setImage("Enemy/faily");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "妖精", "妖精", "妖精" }, status);
        p.setImage("Enemy/faily");


    }

    public void putPresetMapData()
    {
        putEnemies();
        putGolds();

    }

    public void Start()
    {
        loadStatusData();
    }
}
