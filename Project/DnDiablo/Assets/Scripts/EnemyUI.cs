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
            healthBar.slowFill.fillAmount = (stats.healthCurrent / stats.healthMax); //temp
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
