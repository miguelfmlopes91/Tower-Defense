using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Range
{
    public override Debuff GetDebuff()
    {
        return new FireDebuff(Target);
    }

    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.FIRE;

    }
}
