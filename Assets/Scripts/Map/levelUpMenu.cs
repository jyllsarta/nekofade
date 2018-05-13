using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpMenu : UIMenu {

    public LevelUpMenuParameter strength;
    public LevelUpMenuParameter intelligence;
    public LevelUpMenuParameter magicCapacity;
    public LevelUpMenuParameter speed;
    public LevelUpMenuParameter defence;
    public LevelUpMenuParameter vitality;
    public NumeratableText golds;

    public TextMeshProUGUI totalLevel;

    public SirokoStats status;
    public StatusMenu statusMenu;

    public void findStatus()
    {
        if (!status)
        {
            status = FindObjectOfType<SirokoStats>();
        }
    }

    public void refresh()
    {
        findStatus();

        golds.numerate(status.gold);
        statusMenu.refresh();
        totalLevel.text = status.getTotalLevel().ToString();

        strength.setLevel(status.strength);
        intelligence.setLevel(status.intelligence);
        magicCapacity.setLevel(status.magicCapacity);
        speed.setLevel(status.speed);
        defence.setLevel(status.defence);
        vitality.setLevel(status.vitality);
        strength.setCost(status.getLevelupCost(SirokoStats.ParameterKind.STRENGTH));
        intelligence.setCost(status.getLevelupCost(SirokoStats.ParameterKind.INTELLIGENCE));
        magicCapacity.setCost(status.getLevelupCost(SirokoStats.ParameterKind.MAGICCAPACITY));
        speed.setCost(status.getLevelupCost(SirokoStats.ParameterKind.SPEED));
        defence.setCost(status.getLevelupCost(SirokoStats.ParameterKind.DEFENCE));
        vitality.setCost(status.getLevelupCost(SirokoStats.ParameterKind.VITALITY));
        strength.setButtonState(status.canLevelUp(SirokoStats.ParameterKind.STRENGTH));
        intelligence.setButtonState(status.canLevelUp(SirokoStats.ParameterKind.INTELLIGENCE));
        magicCapacity.setButtonState(status.canLevelUp(SirokoStats.ParameterKind.MAGICCAPACITY));
        speed.setButtonState(status.canLevelUp(SirokoStats.ParameterKind.SPEED));
        defence.setButtonState(status.canLevelUp(SirokoStats.ParameterKind.DEFENCE));
        vitality.setButtonState(status.canLevelUp(SirokoStats.ParameterKind.VITALITY));
    }


    // Use this for initialization
    void Start () {
        findStatus();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
