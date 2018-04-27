using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Map : MonoBehaviour {

    public GameObject pointsContainer;
    public List<MapPoint> points;
    public MapPoint currentPoint;
    public float distance;
    public MapSirokoIllust sirokoillust;

    public MapPoint goalPoint;
    public MapPoint bossPoint;

    public MapEventEnemy enemyEventPrefab;

    public SirokoStats status;
    public SirokoStats status_default;

    public DamageEffect healEffect;

    //オブジェクト発生位置指定用
    public NumeratableText hpValue;
    public NumeratableText mpValue;

    public TextMeshProUGUI clock;

    public GetGoldEffect getGoldEffect;
    public GameObject canvas;
    public DialogMenu dialog;

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


    //1マス移動ぶんの回復を適用
    public void applyMoveHealing()
    {
        int hpHealValue = 30;
        if (status.hp < status.maxHp)
        {
            status.healHp(hpHealValue);
            DamageEffect createdHpEffect = Instantiate<DamageEffect>(healEffect, hpValue.transform);
            createdHpEffect.damageText.text = hpHealValue.ToString();
        }

        int mpHealValue = status.magicCapacity*2+5;
        if (status.mp   < status.maxMp)
        {
            status.healMp(mpHealValue);
            DamageEffect createdMpEffect = Instantiate<DamageEffect>(healEffect, mpValue.transform);
            createdMpEffect.transform.Translate(new Vector3(0, 50, 0));
            createdMpEffect.damageText.text = mpHealValue.ToString();
        }
    }
    public void setCurrentPoint(MapPoint p)
    {
        status.tick();
        clock.text = status.clock.ToString();
        applyMoveHealing();
        currentPoint = p;
        sirokoillust.destination = p;
        sirokoillust.anim.Play("MapSirokoJump");
        p.startEvent();
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

    public void playGetGoldEffect(int amount)
    {
        GetGoldEffect created = Instantiate(getGoldEffect, canvas.transform);
        created.set(amount);
        created.transform.position = sirokoillust.transform.position;
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
        bossPoint.mapEvent = new MapEventEnemy(new List<string>() { "魔王" }, status);
        bossPoint.setImage("Enemy/kingNeko"); 

        goalPoint.mapEvent = new MapEventGoal(this, dialog);
        goalPoint.setImage("SimpleVectorIcons/UI_Icon_InputJoystick");

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
        p.mapEvent = new MapEventEnemy(new List<string>() { "妖精", "妖精", "妖精"}, status);
        p.setImage("Enemy/faily");


    }

    //お金のマス置く
    public void putGolds()
    {
        MapPoint p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(30, status, this);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(5, status, this);
        p.setImage("etc/mapgold");
        p = findEmptyMapPoint();
        p.mapEvent = new MapEventGold(200, status, this);
        p.setImage("etc/mapgold");
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
        putGolds();
        DontDestroyOnLoad(status);
    }

    // Update is called once per frame
    void Update()
    {
        updateAvailablePoints();
    }

    public void loadDebugBattleScene()
    {
        SceneManager.LoadSceneAsync("debugBattleSimulator");
    }
}
