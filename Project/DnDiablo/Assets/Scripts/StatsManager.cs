using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatsManager : MonoBehaviour {

    [Header("Upgrade Amounts")]
    [SerializeField] public float healthIncreasePerLevel = 10f;
    [SerializeField] public float healthRegIncreasePerLevel = 0.6f;
    [Space]
    [SerializeField] public float powerIncreasePerLevel = 0.05f;
    [Space]
    [SerializeField] public float resourceIncreasePerLevel = 10f;
    [SerializeField] public float resourceRegIncreasePerLevel = 0.75f;
    [Space]
    [SerializeField] public float cooldownRedIncreasePerLevel = 0.06f;
    [Space]
    [SerializeField] public float moveSpeedIncreasePerLevel = 0.5f;

    [Header("Stat Display Areas")]
    [SerializeField] private Text healthTxt;
    [SerializeField] private Text powerTxt;
    [SerializeField] private Text resourceTxt;
    [SerializeField] private Text cooldownTxt;
    [SerializeField] private Text speedTxt;
    [Space]
    [SerializeField] private Text pointsTxt;

    [Header("Buttons")]
    [SerializeField] private Button healthBtn;
    [SerializeField] private Button powerBtn;
    [SerializeField] private Button resourceBtn;
    [SerializeField] private Button cooldownBtn;
    [SerializeField] private Button speedBtn;
    [Space]
    [SerializeField] private GameObject iconParent;

    [Header("Other")]
    [SerializeField] private GameObject statsUpgradeParent;
    [SerializeField] private KeyCode toggleKey = KeyCode.C;


    public int spendableStatPoints = 0;
    


    #region Singleton
    public static StatsManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion


    public void Update()
    {
        /*if (Input.GetKeyDown(toggleKey))
        {
            ToggleStatsWindow();
        }*/

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

        if (spendableStatPoints > 0)
        {
            healthBtn.interactable = true;
            powerBtn.interactable = true;
            resourceBtn.interactable = true;
            cooldownBtn.interactable = true;
            speedBtn.interactable = true;

            iconParent.SetActive(true);

        }
        else
        {
            healthBtn.interactable = false;
            powerBtn.interactable = false;
            resourceBtn.interactable = false;
            cooldownBtn.interactable = false;
            speedBtn.interactable = false;

            iconParent.SetActive(false);
        }

        pointsTxt.text = spendableStatPoints.ToString();
    }

    public void UpgradeHealth()
    {
        Player.Instance.myStats.healthMax += healthIncreasePerLevel;
        Player.Instance.myStats.healthBase += healthIncreasePerLevel;
        Player.Instance.myStats.healthCurrent += healthIncreasePerLevel;

        Player.Instance.myStats.healthRegCurrent += healthRegIncreasePerLevel;
        Player.Instance.myStats.healthRegBase += healthRegIncreasePerLevel;

        Player.Instance.myStats.healthDisplay++;
        spendableStatPoints--;

        UpdateStatsDisplay();
    }

    public void UpgradePower()
    {
        Player.Instance.myStats.powerBase += powerIncreasePerLevel;
        Player.Instance.myStats.powerCurrent += powerIncreasePerLevel;

        Player.Instance.myStats.powerDisplay++;
        spendableStatPoints--;


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
        spendableStatPoints--;

        UpdateStatsDisplay();
    }

    public void UpgradeCooldownRed()
    {
        Player.Instance.myStats.cooldownRedBase += cooldownRedIncreasePerLevel;
        Player.Instance.myStats.cooldownRedCurrent += cooldownRedIncreasePerLevel;

        Player.Instance.myStats.cooldownRedDisplay++;
        spendableStatPoints--;

        UpdateStatsDisplay();
    }

    public void UpgradeMoveSpeed()
    {
        Player.Instance.myStats.moveSpeedBase += moveSpeedIncreasePerLevel;
        Player.Instance.myStats.moveSpeedCurrent += moveSpeedIncreasePerLevel;

        Player.Instance.myStats.moveSpeedDisplay++;

        Player.Instance.Agent.speed = Player.Instance.myStats.moveSpeedCurrent;
        spendableStatPoints--;

        UpdateStatsDisplay();
    }
}
