using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosionBehaviour : SpellBehaviour {

    public float damage;
    public Vector3 startSize;
    public Vector3 expansionRate;


    // Use this for initialization
    void Start()
    {
        gameObject.transform.localScale = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale += (expansionRate * Time.deltaTime);
        
    }

    private void OnTriggerStay(Collider other)
    {
        IDamageable _damagable = other.gameObject.GetComponent<IDamageable>();

        if (_damagable != null)
        {
            _damagable.TakeDamage((damage*Time.deltaTime), caster);
        }
    }
}
