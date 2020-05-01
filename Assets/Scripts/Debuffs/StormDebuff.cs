using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormDebuff : Debuff
{


    public StormDebuff(Monster target, float duration) : base(target,duration)
    {
        if (target!=null)
        {
            target.Speed = 0;
        }
    }

    public override void RemoveDebuff()
    {
        if (target != null)
        {
            target.Speed = target.MaxSpeed;
        }

        base.RemoveDebuff();
    }

    public override void Update()
    {
        base.Update();
    }

}
