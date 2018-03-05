using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour {
    public int frameWidth = 1600;
    public List<Action> currentActions;
    public int framesPerTurn = 60;

    public void flushCommand()
    {

    }

    public void Add(Action a)
    {
        currentActions.Add(a);
    }

}
