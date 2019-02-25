﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HomingMissile : Skill {

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileDuration;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _projectile = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
        HomingMissileBehaviour _missileBehaviour = _projectile.GetComponent<HomingMissileBehaviour>();


        //Movement
        _missileBehaviour.speed = projectileSpeed;
        _missileBehaviour.damage = projectileDamage;
        _missileBehaviour.maxDuration = projectileDuration;
        _missileBehaviour.casterCollider = caster.GetComponent<Collider>();
    }
}
