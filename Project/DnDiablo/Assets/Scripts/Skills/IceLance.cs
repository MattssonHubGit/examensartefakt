using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IceLance : Skill
{
    public override void Action(Vector3 targetPos, Entity caster)
    {
        Debug.Log("IceLance::Action -- Skill logic not implemented");
    }
}
