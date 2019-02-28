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

    private void Start()
    {
        if (target == null)
            return;

        Vector3 _dir = target.transform.position - caster.transform.position;
        _dir.Normalize();
        transform.rotation = Quaternion.LookRotation(_dir);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        RotationController();
    }

    private void RotationController()
    {
        gfxParent.transform.RotateAround(gfxParent.transform.position, transform.forward, rotateSpeed * Time.deltaTime);
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
