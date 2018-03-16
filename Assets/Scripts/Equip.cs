﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip{

    public int strength;
    public int intelligence;
    public int speed;
    public int defence;
    public int vitality;
    public List<Buff.BuffID> enchants;
    public string description;
    public Equip(int strength, int intelligence, int speed, int defence, int vitality, List<Buff.BuffID> enchants, string description)
    {
        this.strength = strength;
        this.intelligence = intelligence;
        this.speed = speed;
        this.defence = defence;
        this.vitality = vitality;
        this.enchants = enchants;
        this.description = description;
    }
}
