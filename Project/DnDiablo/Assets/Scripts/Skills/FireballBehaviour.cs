using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{

    [Header("Base Projectile")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction = new Vector3();

    [Header("Fireball")]
    [HideInInspector] public float damage = 0;
    [HideInInspector] public float areaRadius = 2;
    [HideInInspector] public float explosionDuration = 0.3f;
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
        SimpleDamageOnTriggerEnter _scrExplosion = _objExplosion.GetComponent<SimpleDamageOnTriggerEnter>();

        _scrExplosion.damageAmount = damage;
        _objExplosion.transform.localScale = new Vector3(areaRadius, areaRadius, areaRadius);

        //Destroy self and explosion
        Destroy(_objExplosion, explosionDuration);
        Destroy(this.gameObject);

    }

}
