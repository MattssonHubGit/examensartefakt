using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]

public class SprintAura : Aura
{
    [Header("Aura specific")]
    public float speedBonusPercentage;
    private float speedBonusAmount;

    public override void OnApply()
    {
        Stats targetStats = target.myStats;
        speedBonusAmount = (target.myStats.moveSpeedCurrent * speedBonusPercentage);

        target.myStats.moveSpeedCurrent += speedBonusAmount;

    }

    public override void OnExpire()
    {
        target.myStats.moveSpeedCurrent -= speedBonusAmount;

    }

    public override void OnTick()
    {
    }
}
