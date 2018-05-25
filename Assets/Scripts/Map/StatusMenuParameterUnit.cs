using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusMenuParameterUnit : MonoBehaviour {

    public TextMeshProUGUI levelText;

    public List<TextMeshProUGUI> blocks;

    public void setLevel(int level, int levelByEquip=0)
    {
        int count = 0;
        int equipBoost = levelByEquip;
        foreach(TextMeshProUGUI block in blocks)
        {
            if (count < level)
            {
                block.text = "■";
                block.color = new Color(1, 1, 1f);
            }
            else if(equipBoost > 0)
            {
                block.text = "■";
                block.color = new Color(1, 1, 0.6f);
                equipBoost--;
            }
            else {
                block.text = "□";
                block.color = new Color(1, 1, 1f);
            }
            count++;
        }
        levelText.text = (level+ levelByEquip).ToString();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
