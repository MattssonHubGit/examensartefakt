using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable {

    [SerializeField] protected Stats myStatsPrefab;
    public Stats myStats;

    public abstract void InitializeStats();

    public void ReduceResource(float amount)
    {
        myStats.resourceCurrent -= amount;
        if (myStats.resourceCurrent < 0)
        {
            myStats.resourceCurrent = 0;
        }
    }

    public virtual void TakeDamage(float amount)
    {
        myStats.healthCurrent -= amount;

        if (myStats.healthCurrent <= 0)
        {
            myStats.healthCurrent = 0;
            OnDeath();
        }
    }

    protected abstract void OnDeath();

    protected abstract void UseSkill(Skill skill);
}
