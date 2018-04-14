using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StatusMenuEquip : MonoBehaviour, IPointerEnterHandler
{

    public TextMeshProUGUI text;
    public MessageArea messageArea;

    public void OnPointerEnter(PointerEventData eventData)
    {
        string equipName = text.text;
        if (text.text == "-" || text.text == null)
        {
            messageArea.updateText("-");
            return;
        }
        Equip e = EquipStore.getEquipByName(equipName);
        messageArea.updateText(e.description);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
