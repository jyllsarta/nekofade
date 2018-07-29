using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public Color physicalDamageColor;
    public Color magicalDamageColor;

    public void set(int value, bool isMagic)
    {
        damageText.text = value.ToString();
        if (isMagic)
        {
            damageText.color = new Color(0.8f, 0.8f, 1f);
        }
        else
        {
            damageText.color = new Color(1f, 0.8f, 0.8f);
        }
    }
}
