using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyUI : MonoBehaviour {

    public static EnemyUI Instance;
    [SerializeField] private Stats stats;
    [SerializeField] private FillingBar healthBar;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);

        #region SingleTon
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion

    }

    // Update is called once per frame
    void Update () {
        if (healthBar != null)
        {
            healthBar.hardFill.fillAmount = (stats.healthCurrent / stats.healthMax);
            if (healthBar.slowFill.fillAmount != healthBar.hardFill.fillAmount)
            {
                healthBar.slowFill.fillAmount = Mathf.Lerp(healthBar.slowFill.fillAmount, healthBar.hardFill.fillAmount, Time.deltaTime * 5f);
            }
        }
    }


    public void SetUpUnit(Stats enemyStats)
    {
        stats = enemyStats;
        gameObject.SetActive(true);


    }

    public void Hide()
    {

        stats = null;
        gameObject.SetActive(false);
    }
}
