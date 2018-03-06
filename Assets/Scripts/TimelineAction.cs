using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//タイムライン上に設置されるアクションひとつを表すオブジェクト
public class TimelineAction : MonoBehaviour {

    //子要素にある画像
    public RectTransform frameImage;    
    
    //子要素にある文字列
    public TextMeshProUGUI text;

    //生成時にtimelineさんがうまく代入してくれます
    public Timeline timeline;

    //このアクションのID
    public int hashCode;

    public void onClick()
    {
        Debug.Log("クリックされたかー");
        timeline.removeAction(hashCode);
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
