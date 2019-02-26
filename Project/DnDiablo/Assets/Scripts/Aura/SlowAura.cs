using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class SlowAura : Aura {

    [SerializeField] public float slowPercentage;
    [HideInInspector] private float slowAmount;

    public override void OnApply()
    {
        Stats targetStats = target.myStats;
        slowAmount = (target.myStats.moveSpeedCurrent * slowPercentage);
        
        target.myStats.moveSpeedCurrent -= slowAmount;
    }

    public override void OnExpire()
    {
        target.myStats.moveSpeedCurrent += slowAmount;
    }

    public override void OnTick()
    {

    }
}
