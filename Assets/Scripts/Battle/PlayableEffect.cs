using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableEffect{
    public Effect effect;
    public Battle.ActorType actortype;
    public int actorIndex;

    public int blockingFrames; 

    public PlayableEffect(Effect effect, Battle.ActorType actortype, int actorIndex)
    {
        this.effect = effect;
        this.actortype = actortype;
        this.actorIndex = actorIndex;
        blockingFrames = effect.blockingFrames;
    }
}
