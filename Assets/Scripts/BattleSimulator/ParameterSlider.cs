using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParameterSlider : MonoBehaviour, IPointerEnterHandler{

    public TextMeshProUGUI levelText;
    public BattleSimulator sim;
    public Slider slider;
    public MessageArea messageArea;
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

    public string getDescription()
    {
        switch (parameterKind)
        {
            case ParameterKind.STRENGTH:
                return "筋力Lv:ちからの強さを表す。1Lvにつき物理ダメージ+40%。";
            case ParameterKind.INTELLIGENCE:
                return "魔力Lv:魔力の強さ、マナを引き出す能力を表す。1Lvにつき魔法ダメージ+40%。";
            case ParameterKind.SPEED:
                return "速度Lv:すばやさを表す。1LvごとにWTを減少。最大のLv5時は行動時間40%カット。";
            case ParameterKind.DEFENCE:
                return "防御Lv:物理障壁を扱う能力。防御のカット率上昇,3Lv到達で障壁を2枚まで貯蓄可能に。";
            case ParameterKind.TOUGHNESS:
                return "体力Lv:へこたれない元気さを表す。1LvにつきHP+40。";
            default:
                return "";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        messageArea.updateText(getDescription());
    }
}
