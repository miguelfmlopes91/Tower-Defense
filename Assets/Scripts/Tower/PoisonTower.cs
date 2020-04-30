using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Range
{
    public override Debuff GetDebuff()
    {
        return new PoisonDebuff(Target);
    }

    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.POISON;

    }
}
