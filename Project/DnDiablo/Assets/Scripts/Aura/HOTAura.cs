using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class HOTAura : Aura
{
    public float healthRegen;
    private Stats targetStats;


    public override void OnApply()
    {
        targetStats = target.myStats;

        targetStats.healthRegCurrent += healthRegen;
    }

    public override void OnExpire()
    {
        targetStats.healthRegCurrent -= healthRegen;
    }

    public override void OnTick()
    {

    }
}
