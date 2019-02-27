using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class TrollBloodAura : Aura
{
    public float healthRegen;
    public float slowPercentage;
    private float slowAmount;
    private Stats targetStats;

    public override void OnApply()
    {
        targetStats = target.myStats;

        slowAmount = (targetStats.moveSpeedCurrent * slowPercentage);
        
        targetStats.moveSpeedCurrent -= slowAmount;
        targetStats.healthRegCurrent += healthRegen;

    }

    public override void OnExpire()
    {
        targetStats.moveSpeedCurrent += slowAmount;
        targetStats.healthRegCurrent -= healthRegen;
    }

    public override void OnTick()
    {
    }
}
