using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    public List<Skill> mySkills = new List<Skill>();

    private void Update()
    {
        SkillCooldownManager();

        SkillController();

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            for (int i = 0; i < mySkills.Count; i++)
            {
                Debug.Log(mySkills[i].name + " " + mySkills[i].level);
            }
            

        }
    }

    private void SkillCooldownManager()
    {
        foreach (Skill skill in mySkills)
        {
            skill.CooldownManager();
        }
    }

    private void SkillController()
    {
        /*for (int i = 0; i < mySkills.Count; i++)
        {
            if (Input.GetButtonDown(i.ToString()))
            {
                Debug.Log(i);
            }
        }*/
    }

    protected override void OnDeath()
    {
        Debug.Log("Player::OnDeath() -- Took lethal damage, but death is not implemented yet!");
    }
}
