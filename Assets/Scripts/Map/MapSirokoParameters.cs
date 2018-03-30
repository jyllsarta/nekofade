using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSirokoParameters : MonoBehaviour {

    public NumeratableSlider hpGauge;
    public NumeratableSlider mpGauge;
    public NumeratableText hpText;
    public NumeratableText mpText;

    public SirokoStats status;

    void Start()
    {
    }
    
    void updateView()
    {
        //各種ゲージがない敵もいる
        if (hpGauge)
        {
            hpGauge.toValue = status.hp;
            hpGauge.setMaxValue(status.maxHp);
        }
        if (hpText)
        {
            hpText.numerate(status.hp);
        }
        if (mpGauge)
        {
            mpGauge.toValue = status.mp;
            mpGauge.setMaxValue(status.maxMp);
        }
        if (mpText)
        {
            mpText.numerate(status.mp);
        }
    }

    //毎フレーム行う処理 
    void Update()
    {
        updateView();
    }
}
