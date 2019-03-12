using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalentNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    [Header("Skill data")]
    [SerializeField] private Skill skillToLevel;
    [SerializeField] private int levelToSet;

    [Header("Self")]
    [SerializeField] private TalentNode previousNode;
    [SerializeField] private TalentNode nextNode;
    [SerializeField] private bool isUnlocked = false;
    [SerializeField] private bool isFirst;
    public Button myButton;

    [Header("Images")]
    public Image icon;
    public Image frame;

    #region GetSetters
    public bool IsUnlocked
    {
        get
        {
            return isUnlocked;
        }
    }

    public bool IsFirst
    {
        get
        {
            return isFirst;
        }

        set
        {
            isFirst = value;
        }
    }

    public Skill SkillToLevel
    {
        get
        {
            return skillToLevel;
        }

        set
        {
            skillToLevel = value;
        }
    }

    public int LevelToSet
    {
        get
        {
            return levelToSet;
        }

        set
        {
            levelToSet = value;
        }
    }

    public TalentNode PreviousNode
    {
        get
        {
            return previousNode;
        }

        set
        {
            previousNode = value;
        }
    }

    public TalentNode NextNode
    {
        get
        {
            return nextNode;
        }

        set
        {
            nextNode = value;
        }
    }

    #endregion


    public void AttemptToLevel()
    {
        //--------------------This first node adds a skill to the player
        if (isFirst)
        {
            if (TalentManager.Instance.spendableTalentPoints > 0) //check if talentpoints are enough
            {
                //Add the skill
                Skill _skillToAdd = Instantiate(skillToLevel);
                _skillToAdd.level = 0;
                _skillToAdd.name += "_player";
                TalentManager.Instance.player.mySkills.Add(_skillToAdd);

                //Generate a skillbutton
                GameObject _objButton = Instantiate(TalentManager.Instance.skillButtonPrefab, Vector3.zero, Quaternion.identity, TalentManager.Instance.skillButtonParent);
                SkillButton _scrButton = _objButton.GetComponent<SkillButton>();

                _scrButton.mySkill = _skillToAdd;
                if(myButton.image.sprite != null) _scrButton.buttonIcon.sprite = icon.sprite;

                TalentManager.Instance.spendableTalentPoints--; //Remove a talentpoint

                //Disable this button and enable the next one
                isUnlocked = true;
                myButton.interactable = false;
                if (nextNode != null)
                {
                    nextNode.myButton.interactable = true;
                    nextNode.icon.color = new Color(1, 1, 1, 1);
                }
                icon.color = new Color(1, 1, 1, 0.3f);
            }
        }


        //-------------------All other nodes level the players skill
        if (TalentManager.Instance.spendableTalentPoints > 0) //check if talentpoints are enough
        {
            for (int i = 0; i < TalentManager.Instance.player.mySkills.Count; i++)
            {
                if (TalentManager.Instance.player.mySkills[i].GetType() == skillToLevel.GetType())
                {
                    TalentManager.Instance.spendableTalentPoints--; //Remove a talentpoint

                    //Ready next button
                    isUnlocked = true;
                    myButton.interactable = false;
                    if (nextNode != null)
                    {
                        nextNode.myButton.interactable = true;
                        nextNode.icon.color = new Color(1, 1, 1, 1);
                    }
                    icon.color = new Color(1, 1, 1, 0.3f);
                    TalentManager.Instance.player.mySkills[i].level = levelToSet;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHovoerUI.Instance.DisplayOnHover(skillToLevel.skillName, skillToLevel.descriptionByLevel[levelToSet]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHovoerUI.Instance.StopDisplayOnHover();
    }
}



