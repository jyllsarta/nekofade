using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUpMenu : MonoBehaviour {

    public Animator animator;
    public levelUpMenuParameter strength;
    public levelUpMenuParameter intelligence;
    public levelUpMenuParameter magicCapacity;
    public levelUpMenuParameter speed;
    public levelUpMenuParameter defence;
    public levelUpMenuParameter vitality;

    public SirokoStats status;

    public void showLevelUpMenu()
    {
        refresh();
        gameObject.SetActive(true);
    }

    public void hideLevelUpMenu()
    {
        gameObject.SetActive(false);
    }


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
    }

    // Use this for initialization
    void Start () {
        findStatus();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
