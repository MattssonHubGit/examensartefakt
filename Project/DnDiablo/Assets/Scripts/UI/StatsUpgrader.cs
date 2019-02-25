using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpgrader : MonoBehaviour {

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
    public static StatsUpgrader Instance;
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

    public void UpdateStatsDisplay()
    {
        healthTxt.text = Player.Instance.myStats.healthBase.ToString();
        powerTxt.text = Player.Instance.myStats.powerBase.ToString();
        resourceTxt.text = Player.Instance.myStats.resourceBase.ToString();
        cooldownTxt.text = Player.Instance.myStats.cooldownRedBase.ToString();
        speedTxt.text = Player.Instance.myStats.moveSpeedBase.ToString();
    }

    public void ToggleStatsWindow()
    {
        UpdateStatsDisplay();
        statsUpgradeParent.SetActive(!statsUpgradeParent.active);
    }
}
