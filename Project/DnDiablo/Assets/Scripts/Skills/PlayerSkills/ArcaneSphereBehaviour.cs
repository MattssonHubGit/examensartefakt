using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneSphereBehaviour : MonoBehaviour {

    [HideInInspector] public float damagePerSec;

    private void OnTriggerStay(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            //Deal damage
            _toDamage.TakeDamage(damagePerSec * Time.deltaTime);

        }
    }

}
