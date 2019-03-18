using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CounterAura : Aura
{
    public override void OnApply()
    {
        target.lookingToCounter = true;
    }

    public override void OnExpire()
    {
        target.lookingToCounter = false;
    }

    public override void OnTick()
    {
        target.lookingToCounter = true;
    }

    public abstract void Counter(Entity caster, Entity target, float damageTaken);
}
