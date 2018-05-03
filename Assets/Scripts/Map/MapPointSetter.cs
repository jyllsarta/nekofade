﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointSetter : MonoBehaviour {

    public Map map;
    public SirokoStats status;

    public MapPoint goalPoint;
    public MapPoint bossPoint; //魔王カニ妖精
    public MapPoint midBossPoint_1; //ゴブLv3
    public MapPoint midBossPoint_2; //妖精ゴブカニ
    public MapPoint treasurePoint_1; //みたま
    public MapPoint treasurePoint_2; //癒陣
    public MapPoint treasurePoint_3; //響火
    public MapPoint treasurePoint_4; //HPポーション
    public MapPoint treasurePoint_5; //ウィンドブーツ
    public MapPoint treasurePoint_6; //双撃
    public MapPoint treasurePoint_7; //プレートアーマー
    public MapPoint goldPoint_1; //300G
    public MapPoint goldPoint_2; //500G


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
        p.mapEvent = new MapEventGold(30, status, map);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(5, status, map);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(30, status, map);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(5, status, map);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(30, status, map);
        p.setImage("etc/mapgold");

    }


    //しろこのいないとこに適当に敵を撒く
    public void putEnemies()
    {

        MapPoint p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "ヤリゴブニキ", "ゴブニキ" }, status);
        p.setImage("Enemy/gob_rance");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "ブルーにゃーさん" }, status);
        p.setImage("Enemy/nya_blue");

        p = findEmptyMapPoint();
        p.mapEvent = new MapEventEnemy(new List<string>() { "妖精", "ゴブニキ" }, status);
        p.setImage("Enemy/faily");


    }

    public void putStaticObjects()
    {
        bossPoint.mapEvent = new MapEventEnemy(new List<string>() { "魔王", "本妖精", "メカカニちゃん" }, status);
        bossPoint.setImage("Enemy/kingNeko");

        midBossPoint_1.mapEvent = new MapEventEnemy(new List<string>() { "鍵妖精", "本妖精", "妖精" }, status);
        midBossPoint_1.setImage("Enemy/faily_eleki");

        midBossPoint_2.mapEvent = new MapEventEnemy(new List<string>() { "メカカニちゃん","メカカニちゃん" }, status);
        midBossPoint_2.setImage("Enemy/kani_white");

        goalPoint.mapEvent = new MapEventGoal(map, map.dialog);
        goalPoint.setImage("SimpleVectorIcons/UI_Icon_InputJoystick");

        treasurePoint_1.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.ITEM, "復活の御魂", map.dialog);
        treasurePoint_1.setImage("SimpleVectorIcons/UI_Icon_Bag1");

        treasurePoint_2.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.ACTION, "癒陣", map.dialog);
        treasurePoint_2.setImage("SimpleVectorIcons/UI_Icon_Bag1");
        treasurePoint_3.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.ACTION, "響火", map.dialog);
        treasurePoint_3.setImage("SimpleVectorIcons/UI_Icon_Bag1");
        treasurePoint_4.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.ITEM, "HPポーション", map.dialog);
        treasurePoint_4.setImage("SimpleVectorIcons/UI_Icon_Bag1");
        treasurePoint_5.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.EQUIP, "ウィンドブーツ", map.dialog);
        treasurePoint_5.setImage("SimpleVectorIcons/UI_Icon_Bag1");
        treasurePoint_6.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.ACTION, "双撃", map.dialog);
        treasurePoint_6.setImage("SimpleVectorIcons/UI_Icon_Bag1");
        treasurePoint_7.mapEvent = new MapEventTreasure(status, map, MapEventTreasure.TreasureType.EQUIP, "プレートアーマー", map.dialog);
        treasurePoint_7.setImage("SimpleVectorIcons/UI_Icon_Bag1");

        goldPoint_1.mapEvent = new MapEventGold(300, status, map);
        goldPoint_1.setImage("etc/mapgoldbig");
        goldPoint_2.mapEvent = new MapEventGold(500, status, map);
        goldPoint_2.setImage("etc/mapgoldbig");

    }

    public void putPresetMapData()
    {
        loadStatusData();
        putStaticObjects();
        putEnemies();
        putGolds();
    }

    public void Start()
    {
        loadStatusData();
    }
}