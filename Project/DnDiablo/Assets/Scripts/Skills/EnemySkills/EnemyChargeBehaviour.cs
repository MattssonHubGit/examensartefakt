using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeBehaviour : SpellBehaviour {

    public float damage;
    public EnemyChargeAura auraToRemove;
    
    private void OnTriggerEnter(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            _toDamage.TakeDamage(damage, caster);
            caster.RemoveAura(auraToRemove);
            Destroy(this.gameObject);
        }
    }
}
