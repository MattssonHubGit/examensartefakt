using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : Enemy {

    [Header("Enemy Specifics")]
    [SerializeField] private GameObject WeaponGO;
    [SerializeField] private bool hasCharge;
    [SerializeField] private Skill chargeSkill;
    [SerializeField] private float minimumChargeRange;

    protected override void Update()
    {
        base.Update();


        //Disable weapon GFX while attacking, enabled it while not attacking
        if (timeAfterAttack > 0)
        {
            WeaponGO.SetActive(false);
        }
        else
        {
            WeaponGO.SetActive(true);
        }

        //If we have a charge, charge while within range
        if (hasCharge && chargeSkill != null)
        {
            //Only handle cooldown if we have a charge skill
            chargeSkill.CooldownManager(myStats);

            //use charge when within range of the charge, but not when to close to the player
            if (distanceToPlayer <= chargeSkill.Range[0] && distanceToPlayer >= minimumChargeRange)
            {
                UseSkill(chargeSkill);

            }
        }

    }
}
