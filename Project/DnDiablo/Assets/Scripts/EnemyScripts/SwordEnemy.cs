using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Enemy {

    [Header("Enemy Specifics")]
    [SerializeField] private GameObject swordGO;

    protected override void Update()
    {
        base.Update();

        if (timeAfterAttack > 0)
        {
            swordGO.SetActive(false);
        }
        else
        {
            swordGO.SetActive(true);
        }
    }
}
