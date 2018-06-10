using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip{

    public int strength;
    public int intelligence;
    public int magicCapacity;
    public int speed;
    public int defence;
    public int vitality;
    public List<Buff.BuffID> enchants;
    public string description;
    public Rarity rarity;

    public enum Rarity
    {
        COMMON,
        RARE,
        EPIC,
        LEGENDARY,
    }

    public static int getCostByRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.COMMON:
                return 200;
            case Rarity.RARE:
                return 300;
            case Rarity.EPIC:
                return 500;
            case Rarity.LEGENDARY:
                return 800;
            default:
                Debug.LogWarning("getCostByRarityのdefaultが呼ばれてる");
                return 999;
        }
    }

    public Equip(int strength, int intelligence, int magicCapacity, int speed, int defence, int vitality, Rarity rarity, List<Buff.BuffID> enchants, string description)
    {
        this.strength = strength;
        this.intelligence = intelligence;
        this.magicCapacity = magicCapacity;
        this.speed = speed;
        this.defence = defence;
        this.vitality = vitality;
        this.rarity = rarity;
        this.enchants = enchants;
        this.description = description;
    }
}
