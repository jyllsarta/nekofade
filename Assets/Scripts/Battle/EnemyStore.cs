﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStore : MonoBehaviour{


    public BattleCharacter prefab;

    public BattleCharacter instanciateEnemyByName(string enemyName,Transform parent)
    {

        BattleCharacter createdChild;
        createdChild = Instantiate(prefab,parent);
        createdChild.characterName = enemyName;
        createdChild.characterNameText.text = enemyName;

        //デフォルトでくっついてるアクションを消す
        createdChild.actions = new List<string>();

        switch (enemyName)
        {
            case "scp":
                createdChild.actions.Add("ひっかき");
                createdChild.actions.Add("爪");
                createdChild.actions.Add("強パンチ");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.initializeParameters(30,0,0,0,0,0,0);
                createdChild.setImage("Enemy/siroNeko");
                return createdChild;
            case "ねこ":
                createdChild.actions.Add("爪");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.initializeParameters(50, 0, 0, 0, 0, 0, 0);
                createdChild.setImage("Enemy/akaNeko");
                return createdChild;
            case "ねこ隊長":
                createdChild.actions.Add("ひっかき");
                createdChild.actions.Add("プチ火炎");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING_RANDOMSTART;
                createdChild.initializeParameters(150, 10, 1, 0, 0, 0, 0);
                createdChild.setImage("Enemy/kingNeko");
                return createdChild;
            case "魔王":
                createdChild.actions.Add("刺突");
                createdChild.actions.Add("魔王剣");
                createdChild.actions.Add("火炎");
                createdChild.actions.Add("刺突");
                createdChild.actions.Add("刺突");
                createdChild.actions.Add("二重障壁");
                createdChild.actions.Add("高揚");
                createdChild.actions.Add("刺突");
                createdChild.actions.Add("魔王剣");
                createdChild.actions.Add("魔王剣");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.addAttribute(CharacterAttribute.AttributeID.ACTIONS_TWICE);
                createdChild.initializeParameters(1310, 10, 2, 2, 6, 3, 1);
                createdChild.setImage("Enemy/kingNeko");
                return createdChild;
            case "ねこベス":
                createdChild.actions.Add("プチ火炎");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.initializeParameters(100, 0, 0, 2, 0, 0, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "ねこ隊長α":
                createdChild.actions.Add("ひっかき");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.initializeParameters(300, 0, 5, 5, 5, 5, 5);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "カニ":
                createdChild.actions.Add("刺突");
                createdChild.actions.Add("刺突");
                createdChild.actions.Add("雷光");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.initializeParameters(100, 0, 1, 1, 1, 9, 2);
                createdChild.setImage("Enemy/kani");
                return createdChild;

            //ここまでデバッグ用敵
            //ここから本番で見る敵
            case "野うさぎ":
                createdChild.actions.Add("てしてし");
                createdChild.actions.Add("ひっかき");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING_RANDOMSTART;
                createdChild.initializeParameters(30, 0, 0, 0, 0, 1, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "スケルトン":
                createdChild.actions.Add("打撃");
                createdChild.actions.Add("ひっかき");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING_RANDOMSTART;
                createdChild.initializeParameters(30, 0, 1, 0, 0, 4, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "スケルトンΩ":
                createdChild.actions.Add("打撃");
                createdChild.actions.Add("ひっかき");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING_RANDOMSTART;
                createdChild.initializeParameters(300, 0, 7, 0, 0, 7, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "アルラウネ":
                createdChild.actions.Add("打撃");
                createdChild.actions.Add("打撃");
                createdChild.actions.Add("火炎");
                createdChild.actions.Add("打撃");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.initializeParameters(600, 0, 1, 0, 0, 0, 0);
                createdChild.setImage("Enemy/siroNeko");
                return createdChild;
            case "従者ガニ":
                createdChild.actions.Add("障壁展開");
                createdChild.actions.Add("障壁展開");
                createdChild.actions.Add("爪");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING_RANDOMSTART;
                createdChild.initializeParameters(90, 0, 1, 0, 0, 6, 0);
                createdChild.setImage("Enemy/kani");
                return createdChild;
            case "ヒグマ":
                createdChild.actions.Add("てしてし");
                createdChild.actions.Add("ひっかき");
                createdChild.actions.Add("てしてし");
                createdChild.actions.Add("噛みつく");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING_RANDOMSTART;
                createdChild.initializeParameters(400, 0, 4, 0, 2, 3, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "ゴンザレス":
                createdChild.actions.Add("打撃");
                createdChild.actions.Add("強化");
                createdChild.actions.Add("打撃");
                createdChild.actions.Add("二重障壁");
                createdChild.actions.Add("火炎");
                createdChild.actions.Add("魔王剣");
                createdChild.actions.Add("強化");
                createdChild.actions.Add("大凍結");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING;
                createdChild.initializeParameters(700, 0, 3, 0, 2, 3, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "妖精":
                createdChild.actions.Add("静電気");
                createdChild.actions.Add("プチ火炎");
                createdChild.routine = BattleCharacter.RoutineType.RANDOM;
                createdChild.initializeParameters(50, 40, 0, 1, 1, 0, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "大妖精":
                createdChild.actions.Add("静電気");
                createdChild.actions.Add("静電気");
                createdChild.actions.Add("静電気");
                createdChild.actions.Add("プチ火炎");
                createdChild.actions.Add("てしてし");
                createdChild.actions.Add("大凍結");
                createdChild.routine = BattleCharacter.RoutineType.RANDOM;
                createdChild.initializeParameters(240, 40, 0, 2, 1, 1, 0);
                createdChild.setImage("Enemy/queenNeko");
                return createdChild;
            case "神":
                createdChild.actions.Add("大凍結");
                createdChild.actions.Add("力溜");
                createdChild.actions.Add("障壁展開");
                createdChild.actions.Add("魔王剣");
                createdChild.actions.Add("大凍結");
                createdChild.actions.Add("魔王剣");
                createdChild.actions.Add("二重障壁");
                createdChild.routine = BattleCharacter.RoutineType.ASCENDING_RANDOMSTART;
                createdChild.addAttribute(CharacterAttribute.AttributeID.ACTIONS_TWICE);
                createdChild.initializeParameters(2400, 40, 5, 5, 5, 5, 5);
                
                createdChild.setImage("Enemy/queenNeko");
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
            case "カニ":
                return "カニ。物防がバカみたいに高いので魔法で倒そう。";
            default:
                return "デフォルトのエネミーの説明だよん";
        }
    }
}