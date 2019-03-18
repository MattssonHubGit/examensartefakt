using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu()]
public class StatIncreaseAura : Aura
{
    [Header("Stats")]
    public StatTypes statToIncrease;
    public int amount;
    public enum StatTypes { HEALTH, POWER, RESOURCE, COOLDOWN, SPEED};


    public override void OnApply()
    {
        for (int i = 0; i < amount; i++)
        {
            switch (statToIncrease)
            {
                case StatTypes.HEALTH:
                    UpgradeHealth(true);
                    break;
                case StatTypes.POWER:
                    UpgradePower(true);
                    break;
                case StatTypes.RESOURCE:
                    UpgradeResource(true);
                    break;
                case StatTypes.COOLDOWN:
                    UpgradeCooldownRed(true);
                    break;
                case StatTypes.SPEED:
                    UpgradeMoveSpeed(true);
                    break;
                default:
                    break;
            }
        }

    }

    public override void OnExpire()
    {
        for (int i = 0; i < amount; i++)
        {
            switch (statToIncrease)
            {
                case StatTypes.HEALTH:
                    UpgradeHealth(false);
                    break;
                case StatTypes.POWER:
                    UpgradePower(false);
                    break;
                case StatTypes.RESOURCE:
                    UpgradeResource(false);
                    break;
                case StatTypes.COOLDOWN:
                    UpgradeCooldownRed(false);
                    break;
                case StatTypes.SPEED:
                    UpgradeMoveSpeed(false);
                    break;
                default:
                    break;
            }
        }

    }

    public override void OnTick()
    {

    }

    private void UpgradeHealth(bool up)
    {
        if (up)
        {
            target.myStats.healthMax += StatsManager.Instance.healthIncreasePerLevel;
            target.myStats.healthBase += StatsManager.Instance.healthIncreasePerLevel;
            target.myStats.healthCurrent += StatsManager.Instance.healthIncreasePerLevel;

            target.myStats.healthRegCurrent += StatsManager.Instance.healthRegIncreasePerLevel;
            target.myStats.healthRegBase += StatsManager.Instance.healthRegIncreasePerLevel;
        }
        else
        {
            target.myStats.healthMax -= StatsManager.Instance.healthIncreasePerLevel;
            target.myStats.healthBase -= StatsManager.Instance.healthIncreasePerLevel;
            target.myStats.healthCurrent -= StatsManager.Instance.healthIncreasePerLevel;

            target.myStats.healthRegCurrent -= StatsManager.Instance.healthRegIncreasePerLevel;
            target.myStats.healthRegBase -= StatsManager.Instance.healthRegIncreasePerLevel;
        }      
    }

    private void UpgradePower(bool up)
    {

        if (up)
        {
            target.myStats.powerBase += StatsManager.Instance.powerIncreasePerLevel;
            target.myStats.powerCurrent += StatsManager.Instance.powerIncreasePerLevel;
        }
        else
        {
            target.myStats.powerBase -= StatsManager.Instance.powerIncreasePerLevel;
            target.myStats.powerCurrent -= StatsManager.Instance.powerIncreasePerLevel;
        }

        
    }

    private void UpgradeResource(bool up)
    {

        if (up)
        {
            target.myStats.resourceMax += StatsManager.Instance.resourceIncreasePerLevel;
            target.myStats.resourceBase += StatsManager.Instance.resourceIncreasePerLevel;
            target.myStats.resourceCurrent += StatsManager.Instance.resourceIncreasePerLevel;

            target.myStats.resourceRegBase += StatsManager.Instance.resourceRegIncreasePerLevel;
            target.myStats.resourceRegCurrent += StatsManager.Instance.resourceRegIncreasePerLevel;
        }
        else
        {
            target.myStats.resourceMax -= StatsManager.Instance.resourceIncreasePerLevel;
            target.myStats.resourceBase -= StatsManager.Instance.resourceIncreasePerLevel;
            target.myStats.resourceCurrent -= StatsManager.Instance.resourceIncreasePerLevel;

            target.myStats.resourceRegBase -= StatsManager.Instance.resourceRegIncreasePerLevel;
            target.myStats.resourceRegCurrent -= StatsManager.Instance.resourceRegIncreasePerLevel;
        }
        
        
    }

    private void UpgradeCooldownRed(bool up)
    {

        if (up)
        {
            target.myStats.cooldownRedBase += StatsManager.Instance.cooldownRedIncreasePerLevel;
            target.myStats.cooldownRedCurrent += StatsManager.Instance.cooldownRedIncreasePerLevel;
        }
        else
        {
            target.myStats.cooldownRedBase -= StatsManager.Instance.cooldownRedIncreasePerLevel;
            target.myStats.cooldownRedCurrent -= StatsManager.Instance.cooldownRedIncreasePerLevel;
        }
        
        
    }

    private void UpgradeMoveSpeed(bool up)
    {
        if(up)
        {
            target.myStats.moveSpeedBase += StatsManager.Instance.moveSpeedIncreasePerLevel;
            target.myStats.moveSpeedCurrent += StatsManager.Instance.moveSpeedIncreasePerLevel;


            target.GetComponent<NavMeshAgent>().speed = target.myStats.moveSpeedCurrent;
        }
        else
        {
            target.myStats.moveSpeedBase -= StatsManager.Instance.moveSpeedIncreasePerLevel;
            target.myStats.moveSpeedCurrent -= StatsManager.Instance.moveSpeedIncreasePerLevel;


            target.GetComponent<NavMeshAgent>().speed = target.myStats.moveSpeedCurrent;
        }

    }
}
