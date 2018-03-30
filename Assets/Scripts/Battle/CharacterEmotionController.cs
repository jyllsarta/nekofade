using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEmotionController : MonoBehaviour {
    public Image currentFace;

    public Sprite orooro,happy,spell,critical,damage,angry,question,doya,normal;

    public int limit;

    public void setEmotion(string emotionName, int backsFrame=-1)
    {
        if (backsFrame > 0)
        {
            limit = backsFrame;
        }
        switch (emotionName)
        {
            case "orooro":
                currentFace.sprite = orooro;
                break;
            case "happy":
                currentFace.sprite = happy;
                break;
            case "spell":
                currentFace.sprite = spell;
                break;
            case "critical":
                currentFace.sprite = critical;
                break;
            case "damage":
                currentFace.sprite = damage;
                break;
            case "angry":
                currentFace.sprite = angry;
                break;
            case "question":
                currentFace.sprite = question;
                break;
            case "doya":
                currentFace.sprite = doya;
                break;
            case "normal":
                currentFace.sprite = normal;
                break;
            default:
                Debug.LogWarning("表情指定ないね");
                break;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        if (limit > 0)
        {
            limit--;
            
        }
        if (limit == 0)
        {
            setEmotion("normal");
            limit = -1;
        }

    }
}
