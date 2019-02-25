using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    [Header("Appearence")]
    [SerializeField] public Image buttonIcon;
    [SerializeField] public Image cooldownOverLay;
    [SerializeField] public Text coolDownText;

    [Header("Data")]
    [HideInInspector] public Skill mySkill;

    private void Update()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (mySkill != null) //Can't do anything without a skill
        {
            cooldownOverLay.fillAmount = (mySkill.CooldownCurrent / mySkill.CooldownMax);
            if (Mathf.CeilToInt(mySkill.CooldownCurrent) != 0)
            {
                coolDownText.text = Mathf.CeilToInt(mySkill.CooldownCurrent/Player.Instance.myStats.cooldownRedCurrent).ToString();
            }
            else
            {
                coolDownText.text = "";
            }
        }
    }

}
