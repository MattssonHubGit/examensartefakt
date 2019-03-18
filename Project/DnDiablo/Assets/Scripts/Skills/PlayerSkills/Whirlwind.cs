﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Whirlwind : Skill {


    [SerializeField] private GameObject swordGO;
    [SerializeField] private float damage;
    [SerializeField] private float numberOfSpins;
    
    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _sword = Instantiate(swordGO, caster.transform.position, (caster.transform.rotation *= Quaternion.Euler(0, 270, 0)));
        WhirlwindBehaviour _whirlwind = _sword.GetComponent<WhirlwindBehaviour>();
        SimpleTransformTracker _tracker = _sword.GetComponent<SimpleTransformTracker>();
        float _fullSwings = ((numberOfSpins * 360)/duration[level]);

        _tracker.target = caster.transform;

        _whirlwind.damage = damage;
        _whirlwind.duration = Duration[0];
        _whirlwind.rotationsSpeed = _fullSwings;
        _whirlwind.caster = caster;
    }
}
