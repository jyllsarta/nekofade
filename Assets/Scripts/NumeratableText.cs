using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//数値を遊戯王のライフポイントみたいにピピピピピッと増減させる
public class NumeratableText : MonoBehaviour {
    public TextMeshProUGUI text;
    public float currentValue;
    public float toValue;
    public float speed;

    public void set(float x)
    {
        currentValue = x;
        toValue = x;
        text.text = x.ToString();
    }
    public void set(int x)
    {
        currentValue = x;
        toValue = x;
        text.text = x.ToString();
    }

    public void numerate(int toValue)
    {
        this.toValue = toValue;
    }
    public void numerate(float toValue)
    {
        this.toValue = toValue;
    }

    void Start()
    {
        speed = 0.1f;
    }

    void Update()
    {
        currentValue = currentValue * (1-speed) + toValue * speed;
        if (Mathf.Abs(currentValue-toValue) < 0.5f)
        {
            currentValue = toValue;
        }
        text.text = ((int)currentValue).ToString();
    }

}
