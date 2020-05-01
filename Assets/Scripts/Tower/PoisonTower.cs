using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Range
{
    public PoisonSplash splashPrefab;
    [SerializeField]
    private float tickTime;

    [SerializeField]
    private int splashDamage;


    public int SplashDamage { get => splashDamage; set => splashDamage = value; }
    public float TickTime { get => tickTime; set => tickTime = value; }

    public override Debuff GetDebuff()
    {
        return new PoisonDebuff(splashDamage,DebuffDuration, tickTime, splashPrefab, Target);
    }

    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.POISON;

    }
}
