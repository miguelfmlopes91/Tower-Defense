using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Range
{
    public override Debuff GetDebuff()
    {
        return new IceDebuff(Target);
    }

    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.ICE;

    }
}
