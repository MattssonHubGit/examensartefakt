﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class Block : Skill {

    [Header("Skill Specific")]
    [SerializeField] private BlockAura blockAuraPrefab;
    [Space]
    [SerializeField] private SlowAura slowAuraPrefab;
    [SerializeField] [Range(0f, 3f)] private List<float> slowPercentageByLevel = new List<float>();
    [Space]
    [SerializeField] private List<bool> increasePowerByLevel = new List<bool>();
    [SerializeField] private int powerAmount;
    [SerializeField] private StatIncreaseAura statIncreaseAuraPrefab;




    public override void Action(Vector3 targetPos, Entity caster)
    {
        //Disable damage
        BlockAura _block = Instantiate(blockAuraPrefab);
        _block.Duration = duration[level];
        caster.AddAura(_block, caster);
        
        //Slow
        SlowAura _slow = Instantiate(slowAuraPrefab);
        _slow.slowPercentage = slowPercentageByLevel[level];
        _slow.Duration = duration[level];
        caster.AddAura(_slow, caster);

        //Power
        StatIncreaseAura _power = Instantiate(statIncreaseAuraPrefab);
        _power.amount = powerAmount;
        _power.statToIncrease = StatIncreaseAura.StatTypes.POWER;
        _power.Duration = duration[level];
        caster.AddAura(_power, caster);
    }
}
