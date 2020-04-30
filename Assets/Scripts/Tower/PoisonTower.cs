using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Range
{
    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.POISON;

    }
}
