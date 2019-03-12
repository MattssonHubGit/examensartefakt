using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealAOEBehaviour : SpellBehaviour {

    public float healAmount;
    public float duration;

    void Update()
    {
        duration -= Time.deltaTime;
        
        if (duration < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IDamageable _damagable = other.gameObject.GetComponent<IDamageable>();

        if (_damagable != null)
        {
            _damagable.TakeDamage(-healAmount * Time.deltaTime, caster);
        }
        
    }
}
