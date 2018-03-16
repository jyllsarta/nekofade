using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsContainer : MonoBehaviour {

    public Buff shieldsPrefab;
    public GameObject shieldsContainer;
    public List<Buff> shields;

    public void initialize(int shieldCount,int currentShield)
    {
        foreach (Buff shield in shields)
        {
            Destroy(shield.gameObject);
        }
        shields.Clear();
        for (int i=0;i<shieldCount;++i)
        {
            shields.Add(Instantiate(shieldsPrefab, shieldsContainer.transform));
        }
        updateShieldCount(currentShield);
    }

    public void updateShieldCount(int count)
    {
        for (int i = 0; i < shields.Count; ++i)
        {
            if (i < count)
            {
                shields[i].icon.color = new Color(0.5f, 0.5f, 1, 1);
            }
            else
            {
                shields[i].icon.color = new Color(255, 255, 255, 1);
            }
        }
    }

}
