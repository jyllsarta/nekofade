using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ListItem : MonoBehaviour, IPointerEnterHandler{

    public TextMeshProUGUI itemNameObject;
    public string itemName;
    public string description;
    public bool isChild;
    public SelectingList parent;
    public MessageArea messageArea;

    public void setName(string data)
    {
        itemNameObject.text = data;
        itemName = data;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        messageArea.updateText(description);
    }

    public void onClick()
    {
        if (isChild)
        {
            parent.removeChild(GetHashCode());
        }
        else
        {
            parent.addChild(itemName, description);
        }
    }
}
