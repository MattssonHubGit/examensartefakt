using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseReactionEnemy : Enemy {

    [Header("Enemy Specifics")]
    [SerializeField] private float reactionDistance;
    [SerializeField] private Skill reactionSkillPrefab;
    [HideInInspector] private Skill reactionSkill;

    protected override void Update()
    {

        base.Update();

        reactionSkill.CooldownManager(myStats);

        if (distanceToPlayer <= reactionDistance)
        {
            UseReactionalSkill();
        }
    }

    protected void UseReactionalSkill()
    {
        //Can I use this skill?
        if (reactionSkill.AttemptCast(this))
        {
            //Use my skill
            reactionSkill.Action(target.position, this);
            timeAfterAttack = skillsToUse[currentSkillIndex].EnemyWindDown[0] + myStats.timeAfterAttack;

        }
    }

    public override void InitializeStats()
    {
        base.InitializeStats();
        reactionSkill = Instantiate(reactionSkillPrefab);
    }
}
