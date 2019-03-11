using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : Skill {

    [SerializeField] private float chargeRange;
    [SerializeField] private float damage;
    [SerializeField] private EnemyChargeAura chargeAura;


    public override void Action(Vector3 targetPos, Entity caster)
    {

    }

    
}
