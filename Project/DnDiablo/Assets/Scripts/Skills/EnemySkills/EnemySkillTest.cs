using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class EnemySkillTest : Skill {

    [SerializeField] private GameObject ProjectilePrefab;
    private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        projectile = Instantiate(ProjectilePrefab, caster.transform.position, Quaternion.identity);
        
    }

}
