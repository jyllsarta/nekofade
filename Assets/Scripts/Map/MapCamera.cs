using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCamera : MonoBehaviour {

    public Camera c;
    public Vector2 prevPos;
    public float sensitivity;

    [SerializeField]
    bool surpressMove;

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
        c.transform.Translate(new Vector3(delta.x, delta.y, 0));
    }

    void constrainPosition()
    {
        Vector3 pos = c.transform.position;
        if (pos.x > 6.7f)
        {
            c.transform.position = new Vector3(6.7f, pos.y, pos.z);
        }
        if (pos.x < -6.6f)
        {
            c.transform.position = new Vector3(-6.6f, pos.y, pos.z);
        }
        pos = c.transform.position;
        if (pos.y > 4.5f)
        {
            c.transform.position = new Vector3(pos.x, 4.5f, pos.z);
        }
        if (pos.y < -3.7f)
        {
            c.transform.position = new Vector3(pos.x, -3.7f, pos.z);
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
        surpressMove = false;
    }

    bool isBattleLoaded()
    {
        if (SceneManager.GetSceneByName("battleAlpha").isLoaded)
        {
            return true;
        }
        return false;
    }

    void FixedUpdate () {
        if (isBattleLoaded())
        {
            return;
        }
        moveCamera();
        constrainPosition();
	}
}
