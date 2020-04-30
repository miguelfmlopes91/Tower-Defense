using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Range
{
    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.STORM;
    }
}
