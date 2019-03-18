using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : SpellBehaviour
{

    [Header("Base Projectile")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction = new Vector3();

    [Header("Fireball")]
    [HideInInspector] public float damage = 0;
    [HideInInspector] public float areaRadius = 2;
    [HideInInspector] public float explosionDuration = 0.3f;
    [HideInInspector] public Vector3 expansionRate;
    [Space]
    [SerializeField] private GameObject explosionPrefab;


    private void Update()
    {
        Projectile.Movement(this.transform, direction, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Create explosion and set up stats
        GameObject _objExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        FireballExplosionBehaviour _scrExplosion = _objExplosion.GetComponent<FireballExplosionBehaviour>();

        _scrExplosion.damage = damage;
        _scrExplosion.caster = caster;
        _scrExplosion.startSize = transform.localScale;

        //Destroy self and explosion
        Destroy(_objExplosion, explosionDuration);
        Destroy(this.gameObject);

    }

}
