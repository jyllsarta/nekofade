using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClearSceneConstructor : MonoBehaviour {

    public ParticleSystem congrats;

    public float congratsFrequency;
    public float congratsTimer;

    public NumeratableText goldReturn;
    public NumeratableText turnCount;
    public TextMeshProUGUI goldNextRank;
    public TextMeshProUGUI turnNextRank;
    public TextMeshProUGUI goldRank;
    public TextMeshProUGUI turnRank;

    public SpriteRenderer darken;

    public SirokoStats status;

    public void updateRanks()
    {
        goldRank.text = RankDefinitions.getRankStringFromGoldAmount(status.gold);
        turnRank.text = RankDefinitions.getRankStringFromClearTurn(status.clock);
        if (goldRank.text == "S")
        {
            goldNextRank.text = "最高評価です！やったね！";
        }
        else
        {
            goldNextRank.text = string.Format("あと {0} でランクアップ", RankDefinitions.getAmountToNextRankFromGoldAmount(status.gold));
        }
        if (turnRank.text == "S")
        {
            turnNextRank.text = "最高評価です！やったね！";
        }
        else
        {
            turnNextRank.text = string.Format("あと {0} でランクアップ", RankDefinitions.getAmountToNextRankFromClearTurn(status.clock));
        }
    }

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
        updateRanks();
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
        foreach (SirokoStats status in FindObjectsOfType<SirokoStats>())
        {
            Destroy(status.gameObject);
        }
        SceneManager.LoadSceneAsync("title");
    }
}
