using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("Stat to Upgrade")]
    [SerializeField] private Stats statToUpgrade;
    public enum Stats { HEALTH, POWER, RESOURCE, COOLDOWN_RED, MOVE_SPEED };
    [SerializeField] private string statName;
    [SerializeField] [TextArea(1, 8)] private string description;
    public void AttemptToUpgrade()
    {
        if (StatsManager.Instance.spendableStatPoints > 0) //Check if upgradepoints are available
        {
            switch (statToUpgrade) //What to upgrade?
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
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHovoerUI.Instance.DisplayOnHover(statName, description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHovoerUI.Instance.StopDisplayOnHover();
    }
}
