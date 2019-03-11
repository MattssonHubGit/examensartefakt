using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChargeAura : Aura {

    public Entity caster;
    public NavMeshAgent agent;
    public Vector3 endPos;

    public override void OnApply()
    {
        agent = caster.GetComponent<NavMeshAgent>();
        agent.speed =
    }

    public override void OnExpire()
    {

    }

    public override void OnTick()
    {

    }
}
