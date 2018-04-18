using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSirokoIllust : MonoBehaviour {

    public MapPoint destination;
    public RectTransform position;
    public float lerpRatio;
    public Sprite normal;
    public Sprite jumping;
    public Animator anim;

    void approachToDestination()
    {
        position.anchoredPosition = Vector2.Lerp(position.anchoredPosition, destination.pos.anchoredPosition,lerpRatio);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        approachToDestination();
	}
}
