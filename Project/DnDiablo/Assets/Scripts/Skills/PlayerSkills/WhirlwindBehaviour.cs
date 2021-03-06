﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindBehaviour : SpellBehaviour
{

    public float damage;
    public float rotationsSpeed;
    public float duration;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, rotationsSpeed * Time.deltaTime);

        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Did I hit something damagable?
        IDamageable _toDamage = other.gameObject.GetComponent<IDamageable>();
        if (_toDamage != null)
        {
            caster.TakeDamage((-1.5f * damage), caster);
            _toDamage.TakeDamage(damage, caster);
        }
    }
}
