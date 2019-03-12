using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealerEnemy : Enemy {


    protected override void StateHandler()
    {
        timeAfterAttack -= Time.deltaTime;
        if (timeAfterAttack <= 0) //after an attack, do nothing untill we should act again
        {

            if (attackDistance >= distanceToPlayer) //within range, do not move, attack
            {
                if (agent.isActiveAndEnabled) //The first time this happens, make sure we don't move
                {
                    agent.destination = transform.position;
                    hasStopped = true;
                }

                transform.LookAt(target);
            }
            else //not within range, activate and move towards player
            {
                timeBeforeAttack = myStats.timeBeforeAttack; //reset this timer

                //Attempt to avoid "jump" when restarting movement, doesn't seem to work that good
                if (hasStopped)
                {
                    NavMeshHit _navMeshHit;
                    NavMesh.SamplePosition(transform.position, out _navMeshHit, 100f, NavMesh.AllAreas);

                    transform.position = _navMeshHit.position;
                    hasStopped = false;
                }
            }

            //Always Use skill on cooldown
            UseSkill(skillsToUse[currentSkillIndex]);
            
        }
    }
}
