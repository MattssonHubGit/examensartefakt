using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{

    public List<Skill> allSkills = new List<Skill>();
    public Dictionary<Skill, int> skillData = new Dictionary<Skill, int>();

    public static TalentManager Instance;
    public Player player;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            //Set up array and don't destroy on load
            SetUpSkillsInDictionary();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void SetUpSkillsInDictionary()
    {
        for (int i = 0; i < allSkills.Count; i++)
        {
            skillData.Add(allSkills[i], 0);
        }
    }

}
