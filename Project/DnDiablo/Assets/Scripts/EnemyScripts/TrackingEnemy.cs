using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemy : Enemy {


    [SerializeField] protected float rotationsSpeed;


    protected override void Movehandler()
    {
        if (agent.isActiveAndEnabled)
        {
            if (!hasStopped && timeAfterAttack <= 0) //Only move if our agent is active
            {
                agent.speed = myStats.moveSpeedCurrent;
                agent.destination = target.transform.position;
            }
            
        }
        //else
        //{
        //    Vector3 targetDir = target.position - transform.position;

        //    // The step size is equal to speed times frame time.
        //    float step = rotationsSpeed * Time.deltaTime;

        //    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        //    // Move our position a step closer to the target.
        //    transform.rotation = Quaternion.LookRotation(newDir);
        //}

    }
    
}
