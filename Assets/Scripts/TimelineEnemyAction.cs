using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimelineEnemyAction : MonoBehaviour {

    //子要素にある文字列
    public TextMeshProUGUI actionName;

    //予測ダメージ量
    public TextMeshProUGUI predictDamage;

    public Image character;

    public RectTransform spellCast;

    //生成時にtimelineさんがうまく代入してくれます
    public Timeline timeline;

    //こいつが指してるアクションのID(削除時に気にする)
    public int hashCode;

    //こいつを発生させたキャラのハッシュ(削除時に気にする)
    public int actorHash;

    public void onClick()
    {
        Debug.Log("クリックされたかー");
        timeline.removeAction(hashCode);
    }
}
