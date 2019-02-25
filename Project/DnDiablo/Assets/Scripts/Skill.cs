﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

    [Header("Cooldown")]
    [SerializeField] protected float cooldownMax = 0.5f;
    protected float cooldownCurrent = 0;
    protected bool cooldownReady = true;

    [Header("Cost")]
    [SerializeField] protected float resourceCost = 20f;

    [Header("Level")]
    public int level = 0;

    [Header("Range")]
    [SerializeField] public int Range;

    [Header("Duration")]
    [SerializeField] protected float duration = 10f;


    #region GetSetters
    public float CooldownCurrent
    {
        get
        {
            return cooldownCurrent;
        }
    }

    public float CooldownMax
    {
        get
        {
            return cooldownMax;
        }
    }

    public float Duration
    {
        get
        {
            return duration;
        }
    }


    #endregion
    public virtual bool AttemptCast(Entity caster)
    {
        if (cooldownReady) // Is cooldown ready?
        {
            if (caster.myStats != null)
            {
                if (caster.myStats.resourceCurrent >= resourceCost)
                {
                    caster.ReduceResource(resourceCost);
                    cooldownCurrent = cooldownMax;
                    return true;
                }
            }
            else
            {
                Debug.LogError("Skills::AttemptCast -- Caster is missing Stats reference");
            }
        }
        return false;
    }

    public void CooldownManager(Stats myStats)
    {

        float _cooldownReduction = myStats.cooldownRedCurrent;

        if (cooldownCurrent <= 0)
        {
            cooldownReady = true;
        }
        else
        {
            cooldownReady = false;
            cooldownCurrent -= (Time.deltaTime * _cooldownReduction);
        }
    }
    public void ResetCooldown()
    {
        cooldownCurrent = 0;
    }

    public abstract void Action(Vector3 targetPos, Entity caster);
}
