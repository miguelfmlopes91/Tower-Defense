using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Range
{
    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    private void Start()
    {
        Set();
        ElementType = ELEMENT.FIRE;

    }

    public override Debuff GetDebuff()
    {
        return new FireDebuff(tickDamage, tickTime, DebuffDuration, Target);
    }

}
