using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class levelUpMenuParameter : MonoBehaviour ,IPointerEnterHandler{

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI toLevelText;
    public TextMeshProUGUI costText;
    public MessageArea messageArea;

    public List<TextMeshProUGUI> blocks;

    public SirokoStats status;

    public SirokoStats.ParameterKind kind;

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

    public void findStatus()
    {
        if (!status)
        {
            status = FindObjectOfType<SirokoStats>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        messageArea.updateText(status.getLevelupText(kind));
    }

    // Use this for initialization
    void Start () {
        findStatus();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
