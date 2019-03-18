using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour, IDamageable {

    [SerializeField] protected Stats myStatsPrefab;
    [HideInInspector] public Stats myStats;
    [SerializeField] protected List<Aura> auraList = new List<Aura>();

    [HideInInspector] public bool lookingToCounter = false;

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canCast = true;
    [HideInInspector] public bool canTakeDamage = true;


    #region GetSetters
    public Stats MyStatsPrefab
    {
        get
        {
            return myStatsPrefab;
        }
    }
    #endregion

    public abstract void InitializeStats();

    public void ReduceResource(float amount)
    {
        myStats.resourceCurrent -= amount;
        if (myStats.resourceCurrent < 0)
        {
            myStats.resourceCurrent = 0;
        }
    }

    public virtual void TakeDamage(float amount, Entity damageDealer)
    {
        if (canTakeDamage)
        {
            float _damageToDeal = (amount * damageDealer.myStats.powerCurrent);
            if (lookingToCounter)
            {
                lookingToCounter = false;
                Counter(damageDealer, _damageToDeal);
                return;
            }

            myStats.healthCurrent -= _damageToDeal;

            if (myStats.healthCurrent <= 0)
            {
                myStats.healthCurrent = 0;
                OnDeath();
            }
        }

    }

    protected virtual void Counter(Entity enemyToTarget, float amount)
    {
        Debug.Log("Entity::Counter() -- Base virtual function called, damage avoided: " + amount);
    }

    protected virtual void Update()
    {
        Aurahandler();
        ManageHealth();
        ManageResource();
    }

    protected virtual void Aurahandler()
    {
        for (int i = 0; i < auraList.Count; i++)
        {
            auraList[i].OnTick();
        }

        for (int i = 0; i < auraList.Count; i++)
        {
            auraList[i].Duration -= Time.deltaTime;
            if (auraList[i].Duration <= 0)
            {
                RemoveAura(auraList[i]);
            }
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

    public virtual void DisableMovement()
    {
    }

    public virtual void EnableMovement()
    {
    }

    public virtual void AddAura(Aura aura, Entity applier)
    {
        auraList.Add(aura);
        aura.target = this;
        aura.applier = applier;
        aura.OnApply();
    }

    public virtual void RemoveAura(Aura aura)
    {
        aura.OnExpire();
        auraList.Remove(aura);
    }
    
}
