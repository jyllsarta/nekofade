using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RankDefinitions {

    public static string getRankStringFromClearTurn(int clearTurn)
    {
        if (clearTurn <= 35)
        {
            return "S";
        }
        if (clearTurn <= 40)
        {
            return "A";
        }
        if (clearTurn <= 50)
        {
            return "B";
        }
        if (clearTurn <= 70)
        {
            return "C";
        }
        if (clearTurn <= 100)
        {
            return "D";
        }
        return "E";
    }

    public static int getAmountToNextRankFromClearTurn(int clearTurn)
    {
        if (clearTurn <= 35)
        {
            Debug.Log("いやもう最強やんけ");
            return 0;
        }
        if (clearTurn <= 40)
        {
            return clearTurn - 35;
        }
        if (clearTurn <= 50)
        {
            return clearTurn - 40;
        }
        if (clearTurn <= 70)
        {
            return clearTurn - 50;
        }
        if (clearTurn <= 100)
        {
            return clearTurn - 70;
        }
        return clearTurn - 100;
    }

    public static string getRankStringFromGoldAmount(int goldAmount)
    {
        if (goldAmount >= 1500)
        {
            return "S";
        }
        if (goldAmount >= 1300)
        {
            return "A";
        }
        if (goldAmount >= 1100)
        {
            return "B";
        }
        if (goldAmount >= 900)
        {
            return "C";
        }
        if (goldAmount >= 700)
        {
            return "D";
        }
        return "E";
    }

    public static int getAmountToNextRankFromGoldAmount(int goldAmount)
    {
        if (goldAmount >= 1500)
        {
            Debug.Log("いやもう最強やんけ");
            return 0;
        }
        if (goldAmount >= 1300)
        {
            return 1500 - goldAmount;
        }
        if (goldAmount >= 1100)
        {
            return 1300 - goldAmount;
        }
        if (goldAmount >= 900)
        {
            return 1100 - goldAmount;
        }
        if (goldAmount >= 700)
        {
            return 900 - goldAmount;
        }
        return 700 - goldAmount;
    }

}
