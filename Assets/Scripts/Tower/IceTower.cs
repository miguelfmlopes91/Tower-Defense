using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Range
{
    private void Start()
    {
        base.Set();
        ElementType = ELEMENT.ICE;

    }
}
