using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneConstructor : MonoBehaviour {

    public ParticleSystem congrats;

    public float congratsFrequency;
    public float congratsTimer;

    public NumeratableText goldReturn;
    public NumeratableText turnCount;

    public SpriteRenderer darken;

    public SirokoStats status;

	// Use this for initialization
	void Start () {
        congratsTimer = 0;

        status = FindObjectOfType<SirokoStats>();

        if (!status)
        {
            Debug.LogWarning("ステータスないよ 直接起動した？");
            return;
        }

        goldReturn.numerate(status.gold);
        turnCount.numerate(status.clock);

	}
	
	// Update is called once per frame
	void Update () {
        congratsTimer += Time.deltaTime;
        if (congratsTimer >= congratsFrequency)
        {
            ParticleSystem con = Instantiate(congrats);
            con.transform.position = new Vector3(Random.Range(-10f,10f), Random.Range(-10f, 10f), -10);
            congratsTimer = 0f;
        }

        darken.color = new Color(0, 0, 0, Mathf.Min(0.75f, Time.timeSinceLevelLoad / 3));
	}

    public void returnToTitle()
    {
        Destroy(status);
        SceneManager.LoadSceneAsync("title");
    }
}
