using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Buff : MonoBehaviour {
    public enum BuffID{
        BLANK,
        POISON,
        GUARD,
        REGENERATE,
        DARK_EROSION,
    }
    public BuffID buffID;
    public int length;
    public string description;
    public Image icon;
    public TextMeshProUGUI text;
    public bool duplicates;
    public bool isPermanent;
    public void setImage(string resourcePath)
    {
        string pathHeader = "SimpleVectorIcons/";
        string path = pathHeader + resourcePath;
        Sprite s = Resources.Load<Sprite>(path);
        icon.sprite = s;
    }
}
