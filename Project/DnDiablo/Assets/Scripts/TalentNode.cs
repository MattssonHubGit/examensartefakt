using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentNode : MonoBehaviour
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

    private void Awake()
    {

    }

    public void AttemptToLevel()
    {
        //--------------------This first node adds a skill to the player
        if (isFirst)
        {
            if (true) //check if talentpoints are enough
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
                if(myButton.image.sprite != null) _scrButton.buttonIcon.sprite = myButton.image.sprite;

                //Disable this button and enable the next one
                isUnlocked = true;
                myButton.interactable = false;
                if (nextNode != null)
                {
                    nextNode.myButton.interactable = true;
                }
            }
        }


        //-------------------All other nodes level the players skill
        if (true) //check if talentpoints are enough
        {
            for (int i = 0; i < TalentManager.Instance.player.mySkills.Count; i++)
            {
                if (TalentManager.Instance.player.mySkills[i].GetType() == skillToLevel.GetType())
                {
                    //Remove talentpoint here
                    isUnlocked = true;
                    myButton.interactable = false;
                    if (nextNode != null)
                    {
                        nextNode.myButton.interactable = true;
                    }
                    TalentManager.Instance.player.mySkills[i].level = levelToSet;
                }
            }
        }
    }


}



