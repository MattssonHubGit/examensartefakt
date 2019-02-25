using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradeButton : MonoBehaviour {

    [Header("Stat to Upgrade")]
    [SerializeField] private Stats statToUpgrade;
    public enum Stats { HEALTH, POWER, RESOURCE, COOLDOWN_RED, MOVE_SPEED };

    public void AttemptToUpgrade()
    {
        if (true) //Check if upgradepoints are available
        {
            switch (statToUpgrade)
            {
                case Stats.HEALTH:
                    {
                        StatsManager.Instance.UpgradeHealth();

                        break;
                    }
                case Stats.POWER:
                    {
                        StatsManager.Instance.UpgradePower();

                        break;
                    }
                case Stats.RESOURCE:
                    {
                        StatsManager.Instance.UpgradeResource();

                        break;
                    }
                case Stats.COOLDOWN_RED:
                    {
                        StatsManager.Instance.UpgradeCooldownRed();

                        break;
                    }
                case Stats.MOVE_SPEED:
                    {
                        StatsManager.Instance.UpgradeMoveSpeed();

                        break;
                    }
                default:
                    {

                        Debug.LogError("StatUpgradeButton::AttemptToUpgrade() -- Reached end of switch, are new states added to the enum?");
                        break;
                    }
            }
        }
    }
}
