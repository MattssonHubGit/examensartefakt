using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStabBehaviour : SpellBehaviour {

    public float damage;
    public float duration;
    public float stabSpeed;

    private void Update()
    {
        //Move us forward
        transform.position += (transform.forward * stabSpeed * Time.deltaTime);
        
        //Remove when duration hits zero
        if (duration <= 0)
        {
            Destroy(this.gameObject);
        }
        duration -= Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            _toDamage.TakeDamage(damage, caster);
        }
    }
}
