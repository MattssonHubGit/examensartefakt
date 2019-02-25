using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable {

    [SerializeField] protected Stats myStatsPrefab;
    [SerializeField] public Stats myStats;

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

    protected void ManageHealth()
    {
        //If i would for some reason start an update with less or equal to 0 health, kill me
        //Mostly just a safety measure 
        if (myStats.healthCurrent <= 0)
        {
            OnDeath();
        }
        //if health is not max, regenerate according to helth regen
        if (myStats.healthCurrent < myStats.healthMax)
        {
            myStats.healthCurrent += (Time.deltaTime * myStats.healthRegCurrent);

        }
        //if health would be higher than maximum, set it to maximum
        if (myStats.healthCurrent > myStats.healthMax)
        {
            myStats.healthCurrent = myStats.healthMax;
        }
    }

    protected void ManageResource()
    {
        //If resource is not maximum, regenerate resources
        if (myStats.resourceCurrent < myStats.resourceMax)
        {
            myStats.resourceCurrent += (Time.deltaTime * myStats.resourceRegCurrent);
        }
        //If resources would be higher than max, set it to maximum
        if (myStats.resourceCurrent > myStats.resourceMax)
        {
            myStats.resourceCurrent = myStats.resourceMax;
        }
    }
}
