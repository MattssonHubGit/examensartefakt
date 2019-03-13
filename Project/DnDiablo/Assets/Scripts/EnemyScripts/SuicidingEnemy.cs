using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidingEnemy : Enemy {

    [SerializeField] protected bool useSkillOnDeath;
    [SerializeField] protected Skill deathSkill;
    [HideInInspector] protected bool hasDied = false;


    protected override void OnDeath()
    {
        base.OnDeath();

        if (useSkillOnDeath && !hasDied)
        {
            hasDied = true;
            UseSkill(deathSkill);
        }
    }



}
