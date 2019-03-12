using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEnemy : Enemy {

    [Header("Enemy Specifics")]
    [SerializeField] private GameObject spearGO;

    protected override void Update()
    {
        base.Update();

        if (timeAfterAttack > 0)
        {
            spearGO.SetActive(false);
        }
        else
        {
            spearGO.SetActive(true);
        }
    }
}
