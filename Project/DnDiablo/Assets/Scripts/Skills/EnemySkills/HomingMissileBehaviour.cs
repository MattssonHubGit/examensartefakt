using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileBehaviour : SpellBehaviour
{

    [Header("Base Projectile")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction = new Vector3();
    [HideInInspector] public float damage = 0;
    [HideInInspector] public Transform target;
    [HideInInspector] public Collider casterCollider;
    [HideInInspector] public float maxDuration;
    [HideInInspector] public Aura aura;


    // Use this for initialization
    void Start()
    {
        target = Player.Instance.transform;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        maxDuration -= Time.deltaTime;
        if (maxDuration < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != casterCollider)
        {
            Entity targetEntity = other.GetComponent<Entity>();
            
            IDamageable _damagable = other.gameObject.GetComponent<IDamageable>();

            if (_damagable != null)
            {
                _damagable.TakeDamage(damage, caster);
            }
            targetEntity.AddAura(aura, caster);

            CameraController.Instance.AddShake(0.1f);
            Destroy(this.gameObject);
        }
    }
}
