using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowBehaviour : SpellBehaviour
{

    [Header("Movement")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool penetrate = false;
    [SerializeField] public float rotateSpeed;

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
        gfxParent.transform.RotateAround(gfxParent.transform.position, transform.right, rotateSpeed *Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            _toDamage.TakeDamage(damage, caster);
            if (!penetrate) //Keep going if penetrating enemies
            {
                CameraController.Instance.AddShake(0.5f);
                Destroy(this.gameObject);
            }
            else
            {
                CameraController.Instance.AddShake(0.1f);
            }
        }
        else //Will not penetrate walls
        {
            Destroy(this.gameObject);
        }
        

    }

}
