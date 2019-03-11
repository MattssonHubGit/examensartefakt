using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class EnemyChargeAura : Aura {

    public Entity caster;
    public Vector3 endPos;
    public float chargeSpeed;
    public float damage;
    [SerializeField] private GameObject chargeGO;

    public override void OnApply()
    {
        caster.DisableMovement();
        GameObject _chargeGO = Instantiate(chargeGO, caster.transform);
        EnemyChargeBehaviour _ECB = _chargeGO.GetComponent<EnemyChargeBehaviour>();
        _ECB.damage = damage;
        _ECB.caster = caster;
    }

    public override void OnExpire()
    {
        
        caster.EnableMovement();
    }

    public override void OnTick()
    {
        if (caster.transform.position == endPos)
        {
            caster.RemoveAura(this);
            return;
        }
        Vector3.MoveTowards(caster.transform.position, endPos, chargeSpeed);
        
        
    }
}
