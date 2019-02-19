using System.Collections;
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

    protected void CooldownManager()
    {
        if (cooldownCurrent <= 0)
        {
            cooldownReady = true;
        }
        else
        {
            cooldownReady = false;
            cooldownCurrent -= Time.deltaTime;
        }

    }

    public abstract void Action(Vector3 targetPos, Entity caster);
}
