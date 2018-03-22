using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumeratableSlider : MonoBehaviour {
    public Slider fasterSlider;
    public Slider slowerSlider;
    public float currentValue;
    public float toValue;
    public float speed;

    public void set(float x)
    {
        currentValue = x;
        toValue = x;
        fasterSlider.value = x;
        slowerSlider.value = x;
    }
    public void set(int x)
    {
        currentValue = x;
        toValue = x;
        fasterSlider.value = x;
        slowerSlider.value = x;
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
        speed = 0.02f;
    }

    void Update()
    {
        currentValue = currentValue * (1 - speed) + toValue * speed;
        if (Mathf.Abs(currentValue - toValue) < 0.5f)
        {
            currentValue = toValue;
        }
        fasterSlider.value = toValue;
        slowerSlider.value = currentValue;
    }

    public void setMaxValue(int value)
    {
        fasterSlider.maxValue = value;
        slowerSlider.maxValue = value;
    }
    public void setMaxValue(float value)
    {
        fasterSlider.maxValue = value;
        slowerSlider.maxValue = value;
    }
}
