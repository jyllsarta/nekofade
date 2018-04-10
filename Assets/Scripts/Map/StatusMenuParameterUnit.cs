using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusMenuParameterUnit : MonoBehaviour {

    public TextMeshProUGUI levelText;

    public List<TextMeshProUGUI> blocks;

    public void setLevel(int level)
    {
        int count = 0;
        foreach(TextMeshProUGUI block in blocks)
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
        levelText.text = level.ToString();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
