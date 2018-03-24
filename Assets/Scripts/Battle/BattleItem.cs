using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BattleItem : MonoBehaviour, IPointerEnterHandler{

    public Image image;
    public TextMeshProUGUI countText;
    public Action action;
    public int count;
    public Battle battle;
    public bool isPassiveItem;
    public string description;
    public string itemName;
    public RectTransform button;
    public MessageArea messageArea;
    public bool isUsed; //このターンもう使ったか

    public void OnPointerEnter(PointerEventData eventData)
    {
        messageArea.updateText(description);
    }

    //使用済
    public void setUsed(bool used)
    {
        this.isUsed = used;
        if (used)
        {
            image.color = new Color(1,1,1,0.5f);
        }
        else
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }

    public void setImage(string resourcePath)
    {
        string pathHeader = "SimpleVectorIcons/";
        string path = pathHeader + resourcePath;
        Sprite s = Resources.Load<Sprite>(path);
        image.sprite = s;
    }

    public void setCount(int count)
    {
        this.count = count;
        countText.text = count.ToString();
    }

    public void showButton()
    {
        if (isPassiveItem)
        {
            return;
        }
        if (isUsed)
        {
            return;
        }
        button.gameObject.SetActive(true);
    }
    public void hideButton()
    {
        button.gameObject.SetActive(false);
    }

    //ターン1のパッシブ効果発動
    public void useItemPassive()
    {
        setCount(count - 1);
        if (count == 0)
        {
            battle.items.Remove(this);
            Destroy(this.gameObject);
        }
        setUsed(true);
    }

    public void useItem()
    {
        setCount(count - 1);
        battle.useItem(this);
        if (count == 0)
        {
            battle.items.Remove(this);
            Destroy(this.gameObject);
        }
        hideButton();
        setUsed(true);
    }
}
