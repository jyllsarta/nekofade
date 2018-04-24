using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRewardsArea : MonoBehaviour {

    public List<BattleReward> rewards;
    public BattleReward prefab;
    public GameObject contentsArea;

    public void  refresh() {
        rewards = new List<BattleReward>();
	}

    public void addReward(BattleReward.RewardType rewardType, int amount, string rewardName="")
    {
        BattleReward createdChild = Instantiate<BattleReward>(prefab, contentsArea.transform);
        createdChild.set(rewardType, amount, rewardName);
        rewards.Add(createdChild);
    }
	
}
