using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimelineEnemyAction : MonoBehaviour, IPointerEnterHandler{

    //子要素にある文字列
    public TextMeshProUGUI actionName;

    //ポインタ乗せた時出てくる説明
    public string description;

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

    public MessageArea messageArea;

    public void onClick()
    {
        Debug.Log("クリックされたかー");
        timeline.removeAction(hashCode);
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
