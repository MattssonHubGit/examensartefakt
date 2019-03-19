using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDamageOnTriggerEnter : MonoBehaviour {

    [SerializeField] public float damageAmount = 0f;
    [SerializeField] public Entity caster;
    [SerializeField] public bool disableSelf = false;
    [SerializeField] public float disableCooldown = 0.3f;
    private float disableTimer = 0;

    private void OnTriggerEnter(Collider other)
    {
        
        IDamageable _damagable = other.gameObject.GetComponent<IDamageable>();

        if (_damagable != null)
        {
            _damagable.TakeDamage(damageAmount, caster);
        }
    }

    private void Update()
    {
        if (disableSelf)
        {
            if (disableTimer > disableCooldown)
            {
                Destroy(this);
            }
            else
            {
                disableTimer += Time.deltaTime;
            }
        }
    }
}
