using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour {

    public GameObject pointsContainer;
    public List<MapPoint> points;
    public MapPoint currentPoint;
    public float distance;
    public MapSirokoIllust sirokoillust;
    public MapSirokoParameters parameters;
    public MapPointSetter mapPreset;

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

    public Button storeButton;

    public Camera camera;
    public EventSystem eventSystem;

    public UISFXController uiSFXController;

    public GameObject downerUI;

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
        sirokoillust.startJump(p);
        p.startEvent();
        updateStoreButtonShowState();
    }

    public void updateStoreButtonShowState()
    {
        if (currentPoint.mapEvent.eventType == MapEvent.EventType.STORE)
        {
            storeButton.gameObject.SetActive(true);
        }
        else
        {
            storeButton.gameObject.SetActive(false);
        }
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
        GetGoldEffect created = Instantiate(getGoldEffect, parameters.transform);
        created.set(amount);
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

    public void refreshStatusArea()
    {
        parameters.refresh();
    }

    // Use this for initialization
    void Start()
    {
        loadStatusData();
        loadGeometry();
        mapPreset.putPresetMapData();
        DontDestroyOnLoad(status);
    }

    public void setEventSystemAndCameraState(bool state)
    {
        camera.gameObject.SetActive(state);
        eventSystem.gameObject.SetActive(state);
    }


    bool isBattleLoaded()
    {
        if (SceneManager.GetSceneByName("battleAlpha").isLoaded)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        updateAvailablePoints();
        if (!isBattleLoaded())
        {
            setEventSystemAndCameraState(true);
            downerUI.SetActive(true);
        }
        else
        {
            setEventSystemAndCameraState(false);
            downerUI.SetActive(false);
        }
    }

    public void loadDebugBattleScene()
    {
        SceneManager.LoadSceneAsync("debugBattleSimulator");
    }
}
