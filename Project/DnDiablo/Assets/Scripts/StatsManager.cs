using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatsManager : MonoBehaviour {

    [Header("Upgrade Amounts")]
    [SerializeField] private float healthIncreasePerLevel = 50f;
    [SerializeField] private float healthRegIncreasePerLevel = 0.5f;
    [Space]
    [SerializeField] private float powerIncreasePerLevel = 0.5f;
    [Space]
    [SerializeField] private float resourceIncreasePerLevel = 50f;
    [SerializeField] private float resourceRegIncreasePerLevel = 0.5f;
    [Space]
    [SerializeField] private float cooldownRedIncreasePerLevel = 0.3f;
    [Space]
    [SerializeField] private float moveSpeedIncreasePerLevel = 2f;

    [Header("Stat Display Areas")]
    [SerializeField] private Text healthTxt;
    [SerializeField] private Text powerTxt;
    [SerializeField] private Text resourceTxt;
    [SerializeField] private Text cooldownTxt;
    [SerializeField] private Text speedTxt;

    [Header("Other")]
    [SerializeField] private GameObject statsUpgradeParent;
    [SerializeField] private KeyCode toggleKey = KeyCode.C;

    #region Singleton
    public static StatsManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion


    public void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleStatsWindow();
        }

    }

    public void ToggleStatsWindow()
    {
        UpdateStatsDisplay();
        statsUpgradeParent.SetActive(!statsUpgradeParent.activeInHierarchy);
    }

    public void UpdateStatsDisplay()
    {
        healthTxt.text = Player.Instance.myStats.healthDisplay.ToString();
        powerTxt.text = Player.Instance.myStats.powerDisplay.ToString();
        resourceTxt.text = Player.Instance.myStats.resourceDisplay.ToString();
        cooldownTxt.text = Player.Instance.myStats.cooldownRedDisplay.ToString();
        speedTxt.text = Player.Instance.myStats.moveSpeedDisplay.ToString();
    }

    public void UpgradeHealth()
    {
        Player.Instance.myStats.healthMax += healthIncreasePerLevel;
        Player.Instance.myStats.healthBase += healthIncreasePerLevel;
        Player.Instance.myStats.healthCurrent += healthRegIncreasePerLevel;

        Player.Instance.myStats.healthRegCurrent += healthRegIncreasePerLevel;
        Player.Instance.myStats.healthRegBase += healthRegIncreasePerLevel;

        Player.Instance.myStats.healthDisplay++;


        UpdateStatsDisplay();
    }

    public void UpgradePower()
    {
        Player.Instance.myStats.powerBase += powerIncreasePerLevel;
        Player.Instance.myStats.powerCurrent += powerIncreasePerLevel;

        Player.Instance.myStats.powerDisplay++;


        UpdateStatsDisplay();
    }

    public void UpgradeResource()
    {
        Player.Instance.myStats.resourceMax += resourceIncreasePerLevel;
        Player.Instance.myStats.resourceBase += resourceIncreasePerLevel;
        Player.Instance.myStats.resourceCurrent += resourceIncreasePerLevel;

        Player.Instance.myStats.resourceRegBase += resourceRegIncreasePerLevel;
        Player.Instance.myStats.resourceRegCurrent += resourceRegIncreasePerLevel;

        Player.Instance.myStats.resourceDisplay++;

        UpdateStatsDisplay();
    }

    public void UpgradeCooldownRed()
    {
        Player.Instance.myStats.cooldownRedBase += cooldownRedIncreasePerLevel;
        Player.Instance.myStats.cooldownRedCurrent += cooldownRedIncreasePerLevel;

        Player.Instance.myStats.cooldownRedDisplay++;

        UpdateStatsDisplay();
    }

    public void UpgradeMoveSpeed()
    {
        Player.Instance.myStats.moveSpeedBase += moveSpeedIncreasePerLevel;
        Player.Instance.myStats.moveSpeedCurrent += moveSpeedIncreasePerLevel;

        Player.Instance.myStats.moveSpeedDisplay++;

        Player.Instance.Agent.speed = Player.Instance.myStats.moveSpeedCurrent;

        UpdateStatsDisplay();
    }
}
