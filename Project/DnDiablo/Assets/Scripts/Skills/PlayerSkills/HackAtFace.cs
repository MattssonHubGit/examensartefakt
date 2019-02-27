using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class HackAtFace : Skill {

    [SerializeField] private GameObject axeGO;
    [SerializeField] private float damage;
    [SerializeField] private float swingSpeed;


    public override void Action(Vector3 targetPos, Entity caster)
    {

        GameObject _axe = Instantiate(axeGO, caster.transform.position, (caster.transform.rotation *= Quaternion.Euler(0, 0, 0)));
        HackAtFaceBehaviour _hack = _axe.GetComponent<HackAtFaceBehaviour>();

        caster.DisableMovement();

        _hack.caster = caster;
        _hack.damage = damage;
        _hack.duration = Duration[0];
        _hack.rotationsSpeed = swingSpeed;

    }
}
