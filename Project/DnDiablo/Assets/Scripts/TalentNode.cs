using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentNode : MonoBehaviour {
    

    [Header("Skill data")]
    [SerializeField] private Skill skillToLevel;
    [SerializeField] private int levelToSet;

    [Header("Self")]
    [SerializeField] private TalentNode previousNode;
    [SerializeField] private TalentNode nextNode;
    [SerializeField] private bool isUnlocked = false;
    public Button myButton;

    private void Awake()
    {
        myButton = this.GetComponent<Button>();
    }

    public void AttemptToLevel()
    {
        if (previousNode.isUnlocked == true) //Check if this previous node is unlocked
        {
            if (isUnlocked == true) //Is this node alreade unlocked?
            {
                myButton.interactable = false;
                return;
            }



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

    #region GetSetters
    public bool IsUnlocked
    {
        get
        {
            return isUnlocked;
        }
    }

    #endregion
}
