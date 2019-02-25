using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDamageOnTriggerEnter : MonoBehaviour {

    [SerializeField] public float damageAmount = 0f;

    private void OnTriggerEnter(Collider other)
    {

        IDamageable _damagable = other.gameObject.GetComponent<IDamageable>();

        if (_damagable != null)
        {
            _damagable.TakeDamage(damageAmount);
        }
    }
}
