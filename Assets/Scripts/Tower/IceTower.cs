using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Range
{
    [SerializeField]
    private float slowingFactor;

    public override Debuff GetDebuff()
    {
        return new IceDebuff(slowingFactor, DebuffDuration, Target);
    }

    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.ICE;

    }
}
