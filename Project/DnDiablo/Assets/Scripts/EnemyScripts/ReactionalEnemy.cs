using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionalEnemy : Enemy {

    [Header("Enemy Specifics")]
    [SerializeField] private Skill reactionSkillPrefab;
    [HideInInspector] private Skill reactionSkill;
    [SerializeField][Range(0f, 1f)] private float healthPercentageToReact;
	
	// Update is called once per frame
	protected override void Update () {

        base.Update();

        reactionSkill.CooldownManager(myStats);

        if (myStats.healthCurrent/myStats.healthMax <= healthPercentageToReact)
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
        }
    }

    public override void InitializeStats()
    {
        base.InitializeStats();
        reactionSkill = Instantiate(reactionSkillPrefab);
    }
}
