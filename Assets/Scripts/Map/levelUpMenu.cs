using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUpMenu : MonoBehaviour {

    public Animator animator;

    public void showLevelUpMenu()
    {
        gameObject.SetActive(true);
    }

    public void hideLevelUpMenu()
    {
        gameObject.SetActive(false);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
