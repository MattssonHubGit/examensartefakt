using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [Header("Appearence")]
    [SerializeField] public Image buttonIcon;
    [SerializeField] public Image cooldownOverLay;
    [SerializeField] public Text coolDownText;

    [Header("Data")]
    [HideInInspector] public Skill mySkill;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHovoerUI.Instance.DisplayOnHover(mySkill.skillName, mySkill.descriptionByLevel[mySkill.level]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHovoerUI.Instance.StopDisplayOnHover();
    }

    private void Update()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (mySkill != null) //Can't do anything without a skill
        {
            cooldownOverLay.fillAmount = (mySkill.CooldownCurrent / mySkill.CooldownMax[mySkill.level]);
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
