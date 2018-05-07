using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {

    public Camera camera;
    public Vector2 prevPos;
    public float sensitivity;

    Vector2 getInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }

    bool isInputStartFrame()
    {
        return (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0);
    }

    void tryTranslateCamera(Vector2 delta)
    {
        camera.transform.Translate(new Vector3(delta.x, delta.y, 0));
    }

    void constrainPosition()
    {
        Vector3 pos = camera.transform.position;
        if (pos.x > 10)
        {
            camera.transform.position = new Vector3(10, pos.y, pos.z);
        }
        if (pos.x < -10)
        {
            camera.transform.position = new Vector3(-10, pos.y, pos.z);
        }
        if (pos.y > 6)
        {
            camera.transform.position = new Vector3(pos.x, 6, pos.z);
        }
        if (pos.y < -5)
        {
            camera.transform.position = new Vector3(pos.x, -5, pos.z);
        }
    }

    void moveCamera()
    {
        if (isInputStartFrame())
        {
            prevPos = getInputPosition();
        }
        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            float dx = (prevPos.x - getInputPosition().x) * sensitivity;
            float dy = (prevPos.y - getInputPosition().y) * sensitivity;
            tryTranslateCamera(new Vector2(dx, dy));
            prevPos = getInputPosition();
        }
    }

	// Use this for initialization
	void Start () {
        prevPos = getInputPosition();
    }

    // Update is called once per frame
    void Update () {
        moveCamera();
        constrainPosition();
	}
}
