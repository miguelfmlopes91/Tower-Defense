using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Range
{
    public override Debuff GetDebuff()
    {
        return new StormDebuff(Target, DebuffDuration);
    }

    public override string GetStats()
    {
        return string.Format("<color=#add8e6ff>{0}</color>{1}", "<Size=20><b>Storm</b></size>", base.GetStats());
    }

    private void Start()
    {
        Set();
        ElementType = ELEMENT.STORM;

        Upgrades = new TowerUpgrade[] {
            new TowerUpgrade(2,2,1,2),
            new TowerUpgrade(5,3,1,2),
        };
    }
}
