using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSirokoIllust : MonoBehaviour {

    public MapPoint destination;
    public RectTransform position;
    public float lerpRatio;

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
