using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusMenu : UIMenu {

    public StatusMenuParameterUnit strength;
    public StatusMenuParameterUnit intelligence;
    public StatusMenuParameterUnit magicCapacity;
    public StatusMenuParameterUnit speed;
    public StatusMenuParameterUnit defence;
    public StatusMenuParameterUnit vitality;

    public TextMeshProUGUI currentGold;

    public SirokoStats status;

    public TextMeshProUGUI totalLevel;

    public BattleActionsArea actionList;

    public List<TextMeshProUGUI> equips;

    public void updateGold()
    {
        currentGold.text = status.gold.ToString();
    }

    public void updateActionsArea()
    {
        actionList.loadUnintaractableActions(status.actions);
    }

    public void updateParameters()
    {
        strength.setLevel(status.getStrengthLevel());
        intelligence.setLevel(status.getIntelligenceLevel());
        magicCapacity.setLevel(status.getMagicCapacityLevel());
        speed.setLevel(status.getSpeedLevel());
        defence.setLevel(status.getDefenceLevel());
        vitality.setLevel(status.getVitalityLevel());
    }

    public void updateEquips()
    {
        for (int i =0;i<3;++i)
        {
            if (i < status.equipments.Count)
            {
                equips[i].text = status.equipments[i];
            }
            else
            {
                equips[i].text = "-";
            }
        }
    }

    public void updateTotalLevel()
    {
        totalLevel.text = status.getTotalLevel().ToString();
    }

    public void refresh()
    {
        updateGold();
        updateActionsArea();
        updateEquips();
        updateParameters();
        updateTotalLevel();
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
