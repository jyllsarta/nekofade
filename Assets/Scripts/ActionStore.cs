using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStore : MonoBehaviour{
    public static Action getActionByName(string actionName="")
    {
        // new Effect文が長くなるのでここで転記しておく(ダサいけど多分読みやすくなるから許して)
        Effect.TargetType TARGET_SINGLE = Effect.TargetType.TARGET_SINGLE;
        Effect.TargetType TARGET_SINGLE_RANDOM = Effect.TargetType.TARGET_SINGLE_RANDOM;
        Effect.TargetType TARGET_ALL = Effect.TargetType.TARGET_ALL;
        Effect.TargetType ME = Effect.TargetType.ME;
        //Effect.TargetType ALLY_SINGLE_RANDOM = Effect.TargetType.ALLY_SINGLE_RANDOM;
        //Effect.TargetType ALLY_ALL = Effect.TargetType.ALLY_ALL;

        Effect.EffectType DAMAGE = Effect.EffectType.DAMAGE;
        Effect.EffectType HEAL = Effect.EffectType.HEAL;
        //Effect.EffectType BUFF = Effect.EffectType.BUFF;

        int waitTime;
        int power;
        Effect e;
        List<Effect> effects = new List<Effect>();
        List<Effect.Attribute> attributes = new List<Effect.Attribute>();

        switch (actionName)
        {
            case "":
                Debug.LogWarning("getActionByNameの空文字コンストラクタが呼ばれてる");
                return new Action();
            case "刺突":
                waitTime = 20;
                power = 17;
                e = new Effect(TARGET_SINGLE, power, DAMAGE);
                return new Action(actionName, waitTime, e);
            case "火炎":
                waitTime = 35;
                power = 80;
                attributes.Add(Effect.Attribute.FIRE);
                e = new Effect(TARGET_SINGLE, power, DAMAGE, attributes);
                return new Action(actionName, waitTime, e);
            case "治癒":
                waitTime = 35;
                power = 80;
                e = new Effect(ME, power, HEAL);
                return new Action(actionName, waitTime, e);
            case "防御":
                waitTime = 10;
                power = 80;
                e = new Effect(ME, power,Buff.BuffID.GUARD);
                return new Action(actionName, waitTime, e);
            case "毒霧":
                waitTime = 15;
                power = 30;
                effects.Add(new Effect(TARGET_ALL, power, DAMAGE));
                effects.Add(new Effect(TARGET_ALL, power, Buff.BuffID.POISON));
                return new Action(actionName, waitTime, effects);
            case "吸収":
                waitTime = 53;
                power = 50;
                effects.Add(new Effect(TARGET_SINGLE, power, DAMAGE));
                effects.Add(new Effect(ME, power / 2, HEAL));
                return new Action(actionName, waitTime, effects);
            case "雷光":
                waitTime = 53;
                power = 11;
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE));
                effects.Add(new Effect(TARGET_SINGLE_RANDOM, power, DAMAGE));
                return new Action(actionName, waitTime, effects);
            default:
                Debug.LogWarning("getActionByNameのdefault:が呼ばれてる");
                return new Action();
        }

    }
}
