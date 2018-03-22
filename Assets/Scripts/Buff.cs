using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[System.Serializable]
public class Buff : MonoBehaviour, IPointerEnterHandler{
    public enum BuffID{
        BLANK,
        POISON,
        GUARD,
        REGENERATE,
        DARK_EROSION,
        CANSEL_ENEMYFIRSTTURN,
        POWERUP,
        STRUP,
        INTUP,
        ENCHANT_FIRE,
        DCS,
        INVINCIBLE,
        SPDUP,
        ADDITIONAL_ATTACK,
        ADDITIONAL_ENDATTACK,
        AUTO_SHIELD,
        ADDITIONAL_MAGIC,
        REGENERATE_MP,
    }
    public BuffID buffID;
    public int length;
    public string description;
    public Image icon;
    public TextMeshProUGUI text;
    public bool duplicates;
    public bool isPermanent;
    public MessageArea messageArea;
    public void setImage(string resourcePath)
    {
        string pathHeader = "SimpleVectorIcons/";
        string path = pathHeader + resourcePath;
        Sprite s = Resources.Load<Sprite>(path);
        icon.sprite = s;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!messageArea)
        {
            return;
        }
        messageArea.updateText(description);
    }

}
