using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerBehaviour : SpellBehaviour {

    [Header("Movement")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool penetrate = false;
    [SerializeField] private float rotateSpeed;

    [Header("Stats")]
    [HideInInspector] public float damage;

    [Header("Components")]
    [SerializeField] private GameObject gfxParent;

    private void Awake()
    {
        RandomizeRotation();
    }

    private void Update()
    {
        Projectile.Movement(this.transform, direction, speed);
        RotationController();
    }

    private void RotationController()
    {
        gfxParent.transform.RotateAround(gfxParent.transform.position, transform.forward, rotateSpeed * Time.deltaTime);
    }

    private void RandomizeRotation()
    {
        int rand = Random.Range(0, 1);
        if (rand == 0)
        {
            rotateSpeed *= -1f;
        }
        else if (rand != 1)
        {
            Debug.LogError("ArrowSprayBehaviour::RandomizeRotation() -- Random.Range(0, 1) returned something other than 1 or 0");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            _toDamage.TakeDamage(damage*Time.deltaTime, caster);
        }
        else //Will not penetrate walls
        {
            if (other.GetComponent<FlamethrowerBehaviour>() == null) //Also does not hit self
            {
                Destroy(this.gameObject);
            }
        }
    }
}
