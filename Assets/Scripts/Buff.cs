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
    }
    public BuffID buffID;
    public int length;
    public string iconFileName;
    public Image icon;
    public TextMeshProUGUI text;
    public bool duplicates;
    public void setImage(string resourcePath)
    {
        string pathHeader = "SimpleVectorIcons/";
        string path = pathHeader + resourcePath;
        Sprite s = Resources.Load<Sprite>(path);
        icon.sprite = s;
    }
}
