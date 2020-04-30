using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Range
{
    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.FIRE;

    }
}
