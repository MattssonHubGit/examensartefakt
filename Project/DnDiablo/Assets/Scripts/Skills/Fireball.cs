using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Fireball : Skill {

    public override void Action(Vector3 targetPos, Entity caster)
    {
        Debug.Log("Fireball::Action -- Skill logic not implemented");
    }
}
