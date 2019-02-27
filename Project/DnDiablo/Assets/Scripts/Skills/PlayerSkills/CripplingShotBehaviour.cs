using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CripplingShotBehaviour : SpellBehaviour
{

    [Header("Movement")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction;
    [SerializeField] private float rotateSpeed;

    [Header("Stats")]
    [HideInInspector] public float damage;
    [HideInInspector] public bool slow;
    [HideInInspector] public float slowPercentage;
    [HideInInspector] public float slowDuration;


    [Header("Components")]
    [SerializeField] private SlowAura slowPrefab;
    [SerializeField] private GameObject gfxParent;
    

    private void Update()
    {
        Projectile.Movement(this.transform, direction, speed);
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
            //Apply effects
            _toDamage.TakeDamage(damage, caster);

            SlowAura _slow = Instantiate(slowPrefab);
            _slow.Duration = slowDuration;
            _slow.slowPercentage = slowPercentage;

            Entity _target = other.GetComponent<Entity>();
            if (_target != null)
            {
                _target.AddAura(_slow, caster);
            }

        }


        Destroy(this.gameObject);
    }
}
