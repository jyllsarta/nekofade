using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//継承前提の表示非表示を管理するもの
public class UIMenu : MonoBehaviour {

    public Animator animator;
    public IUIMenuAction closeHandler;

    public void show()
    {
        gameObject.SetActive(true);
        //animator.Play("show");
    }
    public void hide()
    {
        gameObject.SetActive(false);
        if (closeHandler != null)
        {
            closeHandler.OnClose();
        }
        //animator.Play("hide");
    }
}
