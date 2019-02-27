using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu()]
public class Leap : Skill
{
    [SerializeField] private LeapAura leapAura;
    [SerializeField] private float trajectoryHeight;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        LeapAura _aura = Instantiate(leapAura);

        _aura.caster = caster;
        _aura.Duration = duration[level];
        _aura.startPos = caster.transform.position;
        _aura.endPos = targetPos;
        _aura.trajectoryHeight = trajectoryHeight;
        
        caster.AddAura(_aura);
        
    }
}
