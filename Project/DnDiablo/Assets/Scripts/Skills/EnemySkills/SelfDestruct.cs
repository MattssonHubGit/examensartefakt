using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelfDestruct : Skill {

    [Header("Self Destruct")]
    [SerializeField] private float damage;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionDuration;
    [SerializeField] private Vector3 expansionRate;



    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _explosion = Instantiate(explosionPrefab, caster.transform.position, Quaternion.identity);
        SelfDestructBehaviour _explosionBehaviour = _explosion.GetComponent<SelfDestructBehaviour>();

        _explosionBehaviour.damage = damage;
        _explosionBehaviour.duration = explosionDuration;
        _explosionBehaviour.expansionRate = expansionRate;

        caster.TakeDamage(caster.myStats.healthCurrent);


    }
}
  
