using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

    [Header("Cooldown")]
    [SerializeField] protected List<float> cooldownMax = new List<float>();
    protected float cooldownCurrent = 0;
    protected bool cooldownReady = true;

    [Header("Cost")]
    [SerializeField] protected List<float> resourceCost = new List<float>();

    [Header("Level")]
    public int level = 0;

    [Header("Targetting")]
    [SerializeField] protected List<float> range = new List<float>();
    [SerializeField] protected bool targetGround = false;

    [Header("Duration")]
    [SerializeField] protected List<float> duration = new List<float>();


    #region GetSetters
    public float CooldownCurrent
    {
        get
        {
            return cooldownCurrent;
        }
    }

    public List<float> CooldownMax
    {
        get
        {
            return cooldownMax;
        }
    }

    public List<float> Duration
    {
        get
        {
            return duration;
        }
    }

    public List<float> Range
    {
        get
        {
            return range;
        }
    }

    public bool TargetGround
    {
        get
        {
            return targetGround;
        }
    }


    #endregion
    public virtual bool AttemptCast(Entity caster)
    {
        if (cooldownReady) // Is cooldown ready?
        {
            if (caster.myStats != null)
            {
                if (caster.myStats.resourceCurrent >= resourceCost[level])
                {
                    caster.ReduceResource(resourceCost[level]);
                    cooldownCurrent = cooldownMax[level];
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
