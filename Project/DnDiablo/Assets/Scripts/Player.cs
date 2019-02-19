using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    public List<Skill> mySkills = new List<Skill>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            for (int i = 0; i < mySkills.Count; i++)
            {
                Debug.Log(mySkills[i].name + " " + mySkills[i].level);
            }
        }
    }
}
