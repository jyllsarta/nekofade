using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff {
    public enum BuffID{
        BLANK,
        POISON,
        GUARD,
        REGENERATE,
    }
    public Buff(BuffID buffID, int length)
    {
        this.length = length;
        this.buffID = buffID;
    }
    public BuffID buffID;
    public int length;
}
