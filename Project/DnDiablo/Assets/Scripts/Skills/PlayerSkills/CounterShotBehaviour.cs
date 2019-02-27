using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterShotBehaviour : SpellBehaviour
{

    [Header("Movement")]
    [HideInInspector] public float speed;
    [SerializeField] private float rotateSpeed;
    [HideInInspector] public Entity target;

    [Header("Stats")]
    [HideInInspector] public float damage;

    [Header("Components")]
    [SerializeField] private GameObject gfxParent;


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        RotationController();
    }

    private void RotationController()
    {
        transform.rotation.SetLookRotation(target.transform.position);
        gfxParent.transform.RotateAround(gfxParent.transform.position, transform.up, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            Entity _entTarget = other.GetComponent<Entity>();
            if(_entTarget != null)
            {
                if (_entTarget == target)
                {
                    _entTarget.TakeDamage(damage, caster);
                    Destroy(this.gameObject);
                }
            }
        }


    }
}
