using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DeathLaser : Skill {

    [Header("DeathLaser")]
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float firingDelay;
    [SerializeField] private float firingDuration;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private Vector3 startSize;
    [SerializeField] private Vector3 firingSize;


    public override void Action(Vector3 targetPos, Entity caster)
    {

        GameObject _laserPointer = Instantiate(laserObject, caster.transform.position, caster.transform.rotation);
        DeathLaserBehaviour _laserBehaviour = _laserPointer.GetComponentInChildren<DeathLaserBehaviour>();

        _laserBehaviour.damage = damagePerSecond;
        _laserBehaviour.delay = firingDelay;
        _laserBehaviour.duration = firingDuration;
        _laserBehaviour.startSize = startSize;
        _laserBehaviour.fireSize = firingSize;
        _laserBehaviour.casterCollider = caster.GetComponent<Collider>();
        _laserBehaviour.targetPosition = targetPos;
        _laserBehaviour.caster = caster;

    }
    
}
