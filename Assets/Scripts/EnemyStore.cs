using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStore : MonoBehaviour{


    public BattleCharacter prefab;

    public BattleCharacter getEnemyByName(string enemyName,Transform parent)
    {
        BattleCharacter createdChild;
        createdChild = Instantiate(prefab,parent);

        switch (enemyName)
        {
            case "scp":
                createdChild.actions.Add("刺突");
                return createdChild;
            default:
                Debug.LogWarning("EnemyName素通りしました");
                return createdChild;
        }

    }
    public static string getEnemyDescriptionByName(string enemyName)
    {
        switch (enemyName)
        {
            case "scp":
                return "目の怖いねこ。いや、ねこではない。ねこか、ねこでした。ねこです";
            case "ねこ":
                return "ねこである。弱い。全パラメータが0で囲まれたとしても勝てる設計にする。";
            case "ねこ隊長":
                return "ねこ隊長である。雑魚ではない。初期勇者がちょっとダメージ残るくらい。";
            case "魔王":
                return "最初の壁となるボス。10Lv回復防御強い魔法のうち2つが必要なくらいを狙う。";
            case "ねこベス":
                return "赤いねこ。魔法を使ってくれると嬉しいなあ、いつ実装されんだろ";
            case "ねこ隊長α":
                return "αが描画できるかテストするために生み出された謎のねこ。魔王の3倍くらい強い";
            default:
                return "デフォルトのエネミーの説明だよん";
        }
    }
}
