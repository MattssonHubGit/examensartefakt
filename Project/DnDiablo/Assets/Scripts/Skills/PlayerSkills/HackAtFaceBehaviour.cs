using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackAtFaceBehaviour : SpellBehaviour
{

    public float damage;
    public float rotationsSpeed;
    public float duration;
    
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.right, rotationsSpeed * Time.deltaTime);

        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            caster.EnableMovement();
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            _toDamage.TakeDamage(damage, caster);

            CameraController.Instance.AddShake(0.01f);
        }
    }
}
