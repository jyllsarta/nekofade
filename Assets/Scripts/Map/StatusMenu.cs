using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusMenu : MonoBehaviour {

    public TextMeshProUGUI currentStrength;
    public TextMeshProUGUI currentIntelligence;
    public TextMeshProUGUI currentMagicCapacity;
    public TextMeshProUGUI currentSpeed;
    public TextMeshProUGUI currentDefence;
    public TextMeshProUGUI currentVitality;

    public TextMeshProUGUI currentGold;

    public SirokoStats status;

    public BattleActionsArea actionList;

    public void updateGold()
    {
        currentGold.text = status.gold.ToString();
    }

    public void updateActionsArea()
    {
        actionList.loadUnintaractableActions(status.actions);
    }

    public void refresh()
    {
        updateGold();
        updateActionsArea();
    }

    // Use this for initialization
    void Start () {
        status = FindObjectOfType<SirokoStats>();
        if (!status)
        {
            Debug.LogWarning("ステータス掴みそこねた");
        }
        refresh();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
