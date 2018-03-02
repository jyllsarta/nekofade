using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


//敵味方共通の処理
public class BattleCharacter : MonoBehaviour {

    public string characterName;
    public int hp;
    public int maxHp;
    public List<Buff> buffs;
    //技一覧

    public Slider hpGauge;

    public bool isDead()
    {
        return hp <= 0;
    }

    void Start()
    {
        buffs = new List<Buff>();
        hpGauge.maxValue = maxHp;
        hpGauge.value = maxHp;
    }

    //バフの残存期間を1F減らし、0になったものを削除する
    void updateBuffState()
    {
        //リストのイテレートしながらの削除は怪しいので、セーフなものリストを作ってそれと入れ替える
        List<Buff> newbuffs = new List<Buff>();
        foreach (Buff buff in buffs)
        {
            //残存期間を減らし
            buff.length -= 1;
            //まだ生きているもののみ新リストに放り込む
            if (buff.length > 0)
            {
                newbuffs.Add(buff);
            }
        }
        //バフリストをごっそり挿げ替え
        this.buffs = newbuffs;
    }

    //ターン毎のバフ効果の処理を行う
    void activateTurnBuffEffect()
    {
        //毒状態なら10点のダメージを受ける
        if (buffs.FindIndex(b => b.buffID == Buff.BuffID.POISON) != -1)
        {
            Debug.Log(string.Format("{0}は毒で10ダメージ!", characterName));
            hp -= 10;
        }
    }

    //こいつがここにあるのが正しいかどうかわからないけどとりあえず置いておく
    //1ターンに一度行う処理
    void OnTurnEnd()
    {
        updateBuffState();
        activateTurnBuffEffect();
    }

    void updateView()
    {
        hpGauge.value = hp;
    }

    //毎フレーム行う処理 
    void Update()
    {
        updateView();
    }

}
