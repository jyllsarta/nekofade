using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetGoldEffect : MonoBehaviour {

    public TextMeshProUGUI amount;

    public void set(int amount)
    {
        this.amount.text = "+" + amount.ToString();
    }
}
