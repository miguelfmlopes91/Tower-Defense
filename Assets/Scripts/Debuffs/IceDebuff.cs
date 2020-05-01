using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDebuff : Debuff
{

    private float slowingFactor;

    private bool applied;

    public IceDebuff(float slowingFactor, float duration, Monster target) : base(target, duration)
    {
        this.slowingFactor = slowingFactor;
    }

    public override void RemoveDebuff()
    {
        target.Speed = target.MaxSpeed;
        base.RemoveDebuff();
    }

    public override void Update()
    {
        if (target!= null)
        {
            if (!applied)
            {
                applied = true;
                target.Speed = target.MaxSpeed * slowingFactor/100;
            }
        }

        base.Update();
    }
}
