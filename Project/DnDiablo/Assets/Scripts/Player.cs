using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Player : Entity {

    [Header("Skills")]
    public List<Skill> mySkills = new List<Skill>();
    private KeyCode[] skillKeys = new KeyCode[8] { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
    
    [Header("UI")]
    [SerializeField] private FillingBar healthBar;
    [SerializeField] private FillingBar resourceBar;

    [Header("Components")]
    public static Player Instance;
    private NavMeshAgent agent;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private Animator myAni;

    [Header("Respawn")]
    [SerializeField] private Transform spawnPoint;


    [HideInInspector] private Vector3 savedPosition;

    #region GetSetters
    public NavMeshAgent Agent
    {
        get
        {
            return agent;
        }
    }

    public Animator MyAni
    {
        get
        {
            return myAni;
        }
    }
    #endregion

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
        agent = this.GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        myAni.SetBool("Fly", true);

        StartCoroutine(FindSpawnPoint());
    }

    protected override void Update()
    {
        base.Update();
        MovementController();

        SkillCooldownManager();
        SkillInputController();
        
        UIManager();

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            TakeDamage(50f);
        }
    }

    private void MovementController()
    {
        //If rooted/casting/whatever, can't move.
        if (!canMove)
            return;

        //Click to move
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            agent.speed = myStats.moveSpeedCurrent;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }

    private void SkillCooldownManager()
    {
        foreach (Skill skill in mySkills)
        {
            skill.CooldownManager(myStats);
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
            if (Input.GetKey(skillKeys[i]) && canCast)
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
            transform.LookAt(GetPositionFromMouse(false));
            myAni.Play("Breath_Fs");
            skill.Action(GetPositionFromMouse(skill.TargetGround), this);
        }
    }

    private Vector3 GetPositionFromMouse(bool getFromGround)
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        //It will either cut through the player center, or from the ground.
        Plane playerPlane;
        if (getFromGround)
        {
            playerPlane = new Plane(Vector3.up, groundPoint.position);
        }
        else
        {
            playerPlane = new Plane(Vector3.up, transform.position);
        }

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
        auraList.Clear();
        myStats.healthCurrent = myStats.healthMax;
        myStats.resourceCurrent = myStats.resourceMax;
        foreach(Skill skill in mySkills)
        {
            skill.ResetCooldown();
        }
        agent.destination = spawnPoint.position;
        agent.Warp(spawnPoint.position);
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
            healthBar.hardFill.fillAmount = (myStats.healthCurrent / myStats.healthMax);
            if (healthBar.slowFill.fillAmount != healthBar.hardFill.fillAmount)
            {
                healthBar.slowFill.fillAmount = Mathf.Lerp(healthBar.slowFill.fillAmount, healthBar.hardFill.fillAmount, Time.deltaTime);
            }
        }

        //Resource bar
        if (resourceBar != null)
        {
            resourceBar.hardFill.fillAmount = (myStats.resourceCurrent / myStats.resourceMax);
            if (resourceBar.slowFill.fillAmount != resourceBar.hardFill.fillAmount)
            {
                resourceBar.slowFill.fillAmount = Mathf.Lerp(resourceBar.slowFill.fillAmount, resourceBar.hardFill.fillAmount, Time.deltaTime);
            }
        }
    }

    private IEnumerator FindSpawnPoint()
    {

        yield return new WaitForEndOfFrame();

        spawnPoint = SpawnPoint.Instance.GetComponent<Transform>();

    }

    public override void DisableMovement()
    {
        agent.isStopped = true;
        
    }

    public override void EnableMovement()
    {
        agent.isStopped = false;
    }
}
