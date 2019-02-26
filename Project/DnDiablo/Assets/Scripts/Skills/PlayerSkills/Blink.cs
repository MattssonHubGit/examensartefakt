using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Blink : Skill {
    
    //[Header("Skill Specific")]

    public override void Action(Vector3 targetPos, Entity caster)
    {
        Debug.Log("Blink::Action -- Skill logic not implemented");

        //Get direction to teleport
        Vector3 dir = targetPos - caster.transform.position;



    }
}
