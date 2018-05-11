﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerEnterHandler{

    public TextMeshProUGUI actionName;
    public TextMeshProUGUI mp;
    public TextMeshProUGUI wt;
    public Button button;
    public MessageArea messageArea;
    public Image actionTypeImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!messageArea)
        {
            messageArea = FindObjectOfType<MessageArea>();
        }
        messageArea.updateText(ActionStore.getActionByName(actionName.text).descriptionText);
    }

}
