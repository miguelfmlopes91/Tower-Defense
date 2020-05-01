using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Range
{
    [SerializeField]
    private float slowingFactor;

    public float SlowingFactor { get => slowingFactor; set => slowingFactor = value; }

    public override Debuff GetDebuff()
    {
        return new IceDebuff(SlowingFactor, DebuffDuration, Target);
    }

    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.ICE;

    }
}
