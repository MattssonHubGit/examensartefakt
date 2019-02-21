using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    [Header("Skills")]
    public List<Skill> mySkills = new List<Skill>();
    private KeyCode[] skillKeys = new KeyCode[8] { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    [Header("UI")]
    [SerializeField] private FillingBar healthBar;
    [SerializeField] private FillingBar resourceBar;

    [Header("Components")]
    public static Player Instance;

    private void Awake()
    {
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

        InitializeStats();
    }

    private void Update()
    {
        SkillCooldownManager();
        SkillInputController();

        UIManager();

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

    private void SkillInputController()
    {
        for (int i = 0; i < mySkills.Count; i++)
        {
            //Limit to skillKeys length
            if (i > skillKeys.Length)
            {
                Debug.LogError("Player::SkillInputController -- Player has more skills than they have buttons! Skipping this itteration");
                continue;
            }

            //Attempt to use the skill corresponding to the button pressed button
            if (Input.GetKeyDown(skillKeys[i]))
            {
                UseSkill(mySkills[i]);
            }
        }
    }

    protected override void UseSkill(Skill skill)
    {
        if (skill.AttemptCast(this))
        {
            Debug.Log("Casting skill: " + skill.name);
            skill.Action(GetPositionFromMouse(), this);
        }
        else
        {
            Debug.Log("SkillToExpensive: " + skill.name);
        }
    }

    private Vector3 GetPositionFromMouse()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;

        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);
            return targetPoint;
        }
        else
        {
            Debug.LogError("Player::GetPositionFromMouse -- Raycast missed plane, returning Vector3.zero");
            return Vector3.zero;
        }
    }

    protected override void OnDeath()
    {
        Debug.Log("Player::OnDeath() -- Took lethal damage, but death is not implemented yet!");
    }

    public override void InitializeStats()
    {
        if (myStats == null)
        {
            myStats = Instantiate(myStatsPrefab);
        }
    }

    private void UIManager()
    {
        //Health bar
        if (healthBar != null)
        {
            healthBar.hardFill.fillAmount = myStats.healthCurrent / myStats.healthMax;
            healthBar.slowFill.fillAmount = myStats.healthCurrent / myStats.healthMax; //temp
        }

        //Resource bar
        if (resourceBar != null)
        {
            resourceBar.hardFill.fillAmount = myStats.healthCurrent / myStats.healthMax;
            resourceBar.slowFill.fillAmount = myStats.healthCurrent / myStats.healthMax; //temp
        }

    }
}
