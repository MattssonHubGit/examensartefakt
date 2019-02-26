using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu()]
public class Blink : Skill
{

    public override void Action(Vector3 targetPos, Entity caster)
    {
        //If targetPos is within range, just go there
        float dist = Vector3.Distance(caster.transform.position, targetPos);
        if (dist <= range[level])
        {
            caster.gameObject.GetComponent<NavMeshAgent>().Warp(targetPos);
            return;
        }

        //Otherwise teleport as far as possible
        Vector3 dir = targetPos - caster.transform.position;
        dir.Normalize();
        Vector3 _inRangeTarget = caster.transform.position + (dir * range[level]);

        caster.gameObject.GetComponent<NavMeshAgent>().Warp(_inRangeTarget);

    }
}
