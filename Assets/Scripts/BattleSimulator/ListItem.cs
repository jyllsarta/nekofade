using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour {

    public TextMeshProUGUI itemNameObject;
    public string itemName;
    public bool isChild;
    public SelectingList parent;

    public void setName(string data)
    {
        itemNameObject.text = data;
        itemName = data;
    }

    public void onClick()
    {
        if (isChild)
        {
            parent.removeChild(GetHashCode());
        }
        else
        {
            parent.addChild(itemName);
        }
    }
}
