using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Slash : Skill
{
    [SerializeField] private GameObject swordGO;
    [SerializeField] private float damage;
    [SerializeField] private float swingSpeed;


    public override void Action(Vector3 targetPos, Entity caster)
    {

        GameObject _sword = Instantiate(swordGO, caster.transform.position, (caster.transform.rotation * Quaternion.Euler(0, 270, 0)));
        SlashBehaviour _slash = _sword.GetComponent<SlashBehaviour>();

        _slash.damage = damage;
        _slash.duration = Duration[0];
        _slash.rotationsSpeed = swingSpeed;
        _slash.caster = caster;
        
    }
}
