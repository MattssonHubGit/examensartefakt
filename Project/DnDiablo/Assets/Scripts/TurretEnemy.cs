using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy {

    [SerializeField] private float maxAttackrange;

    protected override void Start()
    {
        base.Start();
        attackDistance = maxAttackrange;
        agent.enabled = false;
        obstacle.enabled = true;

    }

    protected override void StateHandler()
    {
        timeAfterAttack -= Time.deltaTime;

        if (timeAfterAttack <= 0) //after an attack, do nothing untill we should act again
        {
            if (attackDistance >= distanceToPlayer) //within range,attack
            {
                transform.LookAt(target);

                //Determine if we have been in range for long enough
                timeBeforeAttack -= Time.deltaTime;
                if (timeBeforeAttack <= 0)
                {
                    UseSkill(skillToUse);
                }
            }
            else // not within range, reset timeBeforeAttack timer
            {
                timeBeforeAttack = myStats.timeBeforeAttack; 
            }
        }
    }

    protected override void Movehandler()
    {
        //I should never move
    }

    protected override void UseSkill(Skill skill)
    {
        base.UseSkill(skill);
        attackDistance = maxAttackrange;
    }

}
