using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleReward : MonoBehaviour
{

    public enum RewardType
    {
        GOLD,
        ITEM,
        EQUIP,
    }

    public RewardType rewardType;

    public int amount;
    public string rewardName;
    public TextMeshProUGUI amountText;

    public void set(BattleReward.RewardType rewardType, int amount, string rewardName = "")
    {
        this.rewardType = rewardType;
        this.amount = amount;
        this.rewardName = rewardName;
        amountText.text = amount.ToString();
    }
}
