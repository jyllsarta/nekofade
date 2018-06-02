using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BattleItem : MonoBehaviour, IPointerEnterHandler{

    public Item item;
    public Image image;
    public TextMeshProUGUI countText;
    public Battle battle;
    public RectTransform button;
    public MessageArea messageArea;
    public bool isUsed; //このターンもう使ったか

    public void OnPointerEnter(PointerEventData eventData)
    {
        messageArea.updateText(item.description);
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
        string pathHeader = "AssetStoreTools/Bakenekokan/Items/";
        string path = pathHeader + resourcePath;
        Sprite s = Resources.Load<Sprite>(path);
        image.sprite = s;
    }

    public void setCount(int count)
    {
        this.item.count = count;
        countText.text = count.ToString();
    }

    public void showButton()
    {
        Debug.Log("ぶーん");
        if (item.isPassiveItem)
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
        setCount(item.count - 1);
        if (item.count == 0)
        {
            battle.items.Remove(this);
            Destroy(this.gameObject);
        }
        setUsed(true);
    }

    public void useItem()
    {
        setCount(item.count - 1);
        battle.useItem(this);
        if (item.count == 0)
        {
            battle.items.Remove(this);
            Destroy(this.gameObject);
        }
        hideButton();
        setUsed(true);
    }

    public void setItem(Item item)
    {
        this.item = item;
        setCount(item.count);
        setImage(item.imagePath);
    }
}
