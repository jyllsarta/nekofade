using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一方に一覧があってタップするとした回数だけもう片方のリストに追加するリスト
public class SelectingList : MonoBehaviour {

    public List<string> masterData;

    public ListItem listItemPrefab;

    public List<ListItem> childDataInstances;
    public GameObject masterObject;
    public GameObject childObject;

    public MessageArea messageArea;

    public enum ListType {
        EQUIP,
        ENEMY,
        ATTENDANT,
        ACTION,
        ITEM,
    }
    public ListType listType;


    public void init()
    {
        childDataInstances = new List<ListItem>();
        foreach (string s in masterData)
        {
            string description = "";
            switch (listType)
            {
                case ListType.ACTION:
                    description = ActionStore.getActionByName(s).descriptionText;
                    break;
                case ListType.ATTENDANT:
                    description = "同行者さん！完全未定なので固定メッセージです";
                    break;
                case ListType.ENEMY:
                    description = EnemyStore.getEnemyDescriptionByName(s);
                    break;
                case ListType.EQUIP:
                    description = EquipStore.getEquipByName(s).description;
                    break;
                case ListType.ITEM:
                    description = "アイテムでーす";
                    break;
            }
            addMaster(s,description);
        }
    }

    //子要素の追加
    public void addChild(string data, string description)
    {
        ListItem createdChild = Instantiate(listItemPrefab, childObject.transform);
        createdChild.setName(data);
        createdChild.description = description;
        createdChild.isChild = true;
        createdChild.parent = this;
        childDataInstances.Add(createdChild);
        createdChild.messageArea = messageArea;
    }

    //親要素側に追加
    public void addMaster(string data, string description)
    {
        ListItem createdChild = Instantiate(listItemPrefab, masterObject.transform);
        createdChild.setName(data);
        createdChild.description = description;
        createdChild.isChild = false;
        createdChild.parent = this;
        createdChild.messageArea = messageArea;
    }

    //ハッシュ指定で子要素を削除
    public void removeChild(int hashCode)
    {
        //Debug.LogFormat("{0}",hashCode);
        foreach (ListItem l in childDataInstances)
        {
            if (l.GetHashCode() == hashCode)
            {
                Destroy(l.gameObject);
            }
        }
        childDataInstances.RemoveAll(x => (x.GetHashCode() == hashCode));
    }

    public List<string> getChildContents()
    {
        List<string> result = new List<string>();
        foreach(ListItem i in childDataInstances)
        {
            result.Add(i.itemName);
        }
        return result;
    }

	// Use this for initialization
	void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
