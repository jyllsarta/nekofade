using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusButtonLevelupWatcher : MonoBehaviour {

    [SerializeField]
    SirokoStats status;

    [SerializeField]
    GameObject levelUpOption;

	// Use this for initialization
	void Start () {
        status = FindObjectOfType<SirokoStats>();
	}
	
	// Update is called once per frame
	void Update () {
        if (status.canLevelUpAny())
        {
            levelUpOption.gameObject.SetActive(true);
        }
        else
        {
            levelUpOption.gameObject.SetActive(false);
        }
    }
}
