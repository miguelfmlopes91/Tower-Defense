﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Range
{
    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    public float TickDamage { get => tickDamage; set => tickDamage = value; }
    public float TickTime { get => tickTime; set => tickTime = value; }

    private void Start()
    {
        Set();
        ElementType = ELEMENT.FIRE;

        Upgrades = new TowerUpgrade[] {
            new TowerUpgrade(2,2,0.5f,5,-0.1f,1),
            new TowerUpgrade(5,3,0.5f,5,-0.1f,1),
        };


    }

    public override Debuff GetDebuff()
    {
        return new FireDebuff(TickDamage, TickTime, DebuffDuration, Target);
    }

    public override string GetStats()
    {
        if (NextUpgrade != null) //If the next is avaliable
        {
            return string.Format("<color=#ffa500ff>{0}</color>{1} \nTick time: {2} <color=#00ff00ff>{4}</color>\nTick damage: {3} <color=#00ff00ff>+{5}</color>", "<size=20><b>Fire</b></size> ", base.GetStats(), TickTime, TickDamage, NextUpgrade.TickTime, NextUpgrade.SpecialDamage);
        }

        //Returns the current upgrade
        return string.Format("<color=#ffa500ff>{0}</color>{1} \nTick time: {2}\nTick damage: {3}", "<size=20><b>Fire</b></size> ", base.GetStats(), TickTime, TickDamage);
    }

    public override void Upgrade()
    {
        tickTime -= NextUpgrade.TickTime;
        tickDamage -= NextUpgrade.SpecialDamage;
        base.Upgrade();
    }
}
