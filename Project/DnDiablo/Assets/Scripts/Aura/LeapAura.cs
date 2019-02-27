using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu()]
public class LeapAura : Aura
{
    public Entity caster;
    public NavMeshAgent agent;
    public Vector3 startPos;
    public Vector3 endPos;
    public float trajectoryHeight;
    private float incrementor;

    public override void OnApply()
    {
        agent = caster.GetAgent();
        agent.enabled = false;
        incrementor = 0;

    }

    public override void OnExpire()
    {
        caster.transform.position = endPos;
        agent.enabled = true;
        agent.Warp(caster.transform.position);
    }

    public override void OnTick()
    {
        if (caster.transform.position == endPos)
        {
            caster.RemoveAura(this);
        }
        // calculate current time within our lerping time range
        incrementor += 0.02f;

        // calculate straight-line lerp position:
        Vector3 currentPos = Vector3.Lerp(startPos, endPos, incrementor);

        // add a value to Y, using Sine to give a curved trajectory in the Y direction
        currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(incrementor) * Mathf.PI);

        // finally assign the computed position to our gameObject:
        caster.transform.position = currentPos;
    }
}
