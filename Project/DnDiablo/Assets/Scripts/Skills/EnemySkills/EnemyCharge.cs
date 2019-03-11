using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyCharge : Skill {

    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeRange;
    [SerializeField] private float damage;
    [SerializeField] private EnemyChargeAura chargeAura;


    public override void Action(Vector3 targetPos, Entity caster)
    {
        EnemyChargeAura _aura = Instantiate(chargeAura);


        Vector3 dir = targetPos - caster.transform.position;
        dir.Normalize();
        Vector3 _targetPos = caster.transform.position + (dir * chargeRange);

        _aura.endPos = _targetPos;
        _aura.chargeSpeed = chargeSpeed;
        _aura.damage = damage;
        _aura.caster = caster;

        caster.AddAura(_aura, caster);

    }
}
