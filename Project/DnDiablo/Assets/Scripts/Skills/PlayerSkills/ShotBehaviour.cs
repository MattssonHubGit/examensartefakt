using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : SpellBehaviour
{

    [Header("Movement")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool penetrate = false;
    [SerializeField] private float rotateSpeed;

    [Header("Stats")]
    [HideInInspector] public float damage;

    [Header("Components")]
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
        Debug.Log(other.name);
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            Debug.Log(damage);
            Debug.Log("_toDamage != null");
            _toDamage.TakeDamage(damage, caster);
            if (!penetrate) //Keep going if penetrating enemies
            {

                Debug.Log("_toDamage != null && !penetrate");
                Destroy(this.gameObject);
            }
        }
        else //Will not penetrate walls
        {

            Debug.Log("_toDamage == null");
            Destroy(this.gameObject);
        }
    }

}
