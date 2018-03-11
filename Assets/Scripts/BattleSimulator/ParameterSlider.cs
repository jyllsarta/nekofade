using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ParameterSlider : MonoBehaviour {

    public TextMeshProUGUI levelText;
    public BattleSimulator sim;
    public Slider slider;
    public ParameterKind parameterKind;

    public enum ParameterKind
    {
        STRENGTH,
        INTELLIGENCE,
        SPEED,
        DEFENCE,
        TOUGHNESS,
    }

    public void sliderHandler()
    {
        levelText.text = slider.value.ToString();
        switch (parameterKind)
        {
            case ParameterKind.STRENGTH:
                sim.siroko.strength = (int)slider.value;
                break;
            case ParameterKind.INTELLIGENCE:
                sim.siroko.intelligence = (int)slider.value;
                break;
            case ParameterKind.SPEED:
                sim.siroko.speed = (int)slider.value;
                break;
            case ParameterKind.TOUGHNESS:
                sim.siroko.toughness= (int)slider.value;
                break;
            case ParameterKind.DEFENCE:
                sim.siroko.defence = (int)slider.value;
                break;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
