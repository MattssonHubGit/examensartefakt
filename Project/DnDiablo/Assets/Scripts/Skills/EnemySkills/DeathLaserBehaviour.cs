﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLaserBehaviour : SpellBehaviour {

    public float damage;
    public float delay;
    public float duration;
    public Vector3 startSize;
    public Vector3 fireSize;
    private Collider ownCollider;
    public Collider casterCollider;
    public Vector3 targetPosition;


	// Use this for initialization
	void Start () {
        transform.localScale = startSize;
        ownCollider = GetComponent<Collider>();
        ownCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            transform.localScale = fireSize;
            ownCollider.enabled = true;
            duration -= Time.deltaTime;
        }
        if (duration < 0)
        {
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other != casterCollider)
        {
            IDamageable _damagable = other.gameObject.GetComponent<IDamageable>();

            if (_damagable != null)
            {
                _damagable.TakeDamage(damage*Time.deltaTime, caster);
            }
            CameraController.Instance.AddShake(1.6f*Time.deltaTime);
            
        }
    }
}
