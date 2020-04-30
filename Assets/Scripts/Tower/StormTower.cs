using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Range
{
    public override Debuff GetDebuff()
    {
        return new StormDebuff(Target);
    }

    private void Start()
    {
        Set();
        ElementType = ELEMENT.STORM;
    }
}
