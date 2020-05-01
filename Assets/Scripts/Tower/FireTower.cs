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

    }

    public override Debuff GetDebuff()
    {
        return new FireDebuff(TickDamage, TickTime, DebuffDuration, Target);
    }

}
