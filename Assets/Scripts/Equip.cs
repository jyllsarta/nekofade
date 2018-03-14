using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip{

    public int strength;
    public int intelligence;
    public int speed;
    public int defence;
    public int toughness;
    public List<Buff.BuffID> enchants;
    public Equip(int strength, int intelligence, int speed, int defence, int toughness, List<Buff.BuffID> enchants)
    {
        this.strength = strength;
        this.intelligence = intelligence;
        this.speed = speed;
        this.defence = defence;
        this.toughness = toughness;
        this.enchants = enchants;
    }
}
