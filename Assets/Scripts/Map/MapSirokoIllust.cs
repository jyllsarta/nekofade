﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSirokoIllust : MonoBehaviour {

    public MapPoint destination;
    public Transform position;
    public float lerpRatio;
    public Sprite normal;
    public Sprite jumping;
    public Animator anim;
    public GameObject jumpEffectPrefab;

    void approachToDestination()
    {
        position.position = Vector2.Lerp(position.position, destination.pos.position,lerpRatio);
    }

    void playJumpPuffEffect()
    {
        GameObject created = Instantiate(jumpEffectPrefab);
        created.transform.position = this.transform.position;
    }

    public void startJump(MapPoint destination)
    {
        this.destination = destination;
        anim.Play("MapSirokoJump");
    }

    public void updateFlipState()
    {
        if (transform.position.x > destination.transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        approachToDestination();
        updateFlipState();
	}
}
