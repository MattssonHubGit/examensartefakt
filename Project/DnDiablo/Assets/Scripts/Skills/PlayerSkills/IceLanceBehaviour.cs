using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLanceBehaviour : SpellBehaviour
{

    [Header("Movement")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool penetrate = false;

    [Header("Stats")]
    [HideInInspector] public float damage;
    [HideInInspector] public bool slow;
    [HideInInspector] public float slowPercentage;
    [HideInInspector] public float slowDuration;


    [Header("Components")]
    [SerializeField] private SlowAura slowPrefab;


    private void Update()
    {
        Projectile.Movement(this.transform, direction, speed);
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


            //Keep going if penetrating enemies
            if (!penetrate) 
            {
                Destroy(this.gameObject);
            }
        }
        else //Will not penetrate walls
        {
            Destroy(this.gameObject);
        }


    }

}
