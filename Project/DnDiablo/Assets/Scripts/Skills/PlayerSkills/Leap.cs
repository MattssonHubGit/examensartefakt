using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu()]
public class Leap : Skill
{
    [SerializeField] private LeapAura leapAura;
    [SerializeField] private float trajectoryHeight;

    public override bool AttemptCast(Entity caster)
    {
        if (cooldownReady && caster.GetComponent<NavMeshAgent>() != null && caster.GetComponent<NavMeshAgent>().isActiveAndEnabled) // Is cooldown ready?
        {
            if (caster.myStats != null)
            {
                if (caster.myStats.resourceCurrent >= resourceCost[level])
                {
                    caster.ReduceResource(resourceCost[level]);
                    cooldownCurrent = cooldownMax[level];
                    return true;
                }
            }
            else
            {
                Debug.LogError("Skills::AttemptCast -- Caster is missing Stats reference");
            }
        }
        return false;
    }

    public override void Action(Vector3 targetPos, Entity caster)
    {
        LeapAura _aura = Instantiate(leapAura);

        //If targetPos is within range, jump there
        float dist = Vector3.Distance(caster.transform.position, targetPos);
        if (dist <= range[level])
        {
            _aura.endPos = targetPos;
        }
        else
        {
            //Otherwise, jump as far as possible
            Vector3 dir = targetPos - caster.transform.position;
            dir.Normalize();
            Vector3 _inRangeTarget = caster.transform.position + (dir * range[level]);

            _aura.endPos = _inRangeTarget;
        }

        _aura.caster = caster;
        _aura.Duration = duration[level];
        _aura.startPos = caster.transform.position;
        
        _aura.trajectoryHeight = trajectoryHeight;

        caster.AddAura(_aura, caster);
            
    }
}
