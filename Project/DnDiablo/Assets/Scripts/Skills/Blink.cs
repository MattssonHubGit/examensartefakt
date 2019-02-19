using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Blink : Skill {
    
    public override void Action(Vector3 targetPos, Entity caster)
    {
        Debug.Log("Blink::Action -- Skill logic not implemented");
    }
}
