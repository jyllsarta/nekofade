using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class levelUpMenuParameter : MonoBehaviour {

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI toLevelText;
    public TextMeshProUGUI costText;

    public List<TextMeshProUGUI> blocks;

    public void setLevel(int level)
    {
        int count = 0;
        foreach (TextMeshProUGUI block in blocks)
        {
            if (count < level)
            {
                block.text = "■";
            }
            else
            {
                block.text = "□";
            }
            count++;
        }
        currentLevelText.text = level.ToString();
        toLevelText.text = level == 5 ? "-" : (level+1).ToString();
    }

    public void setCost(int cost)
    {
        costText.text = cost.ToString();
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
