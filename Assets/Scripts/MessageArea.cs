using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//画面上に張り付いてる説明欄
public class MessageArea : MonoBehaviour
{

    public TextMeshProUGUI textarea;

    public void updateText(string text)
    {
        textarea.text = text;
    }
}
