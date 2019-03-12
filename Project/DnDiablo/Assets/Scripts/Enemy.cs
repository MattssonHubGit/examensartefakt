using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Collections;
using System;

public class Enemy : Entity {

    [Header("initialization")]
    protected NavMeshAgent agent;
    protected Collider ownCollider;
    [SerializeField] protected Transform target;
    [SerializeField] protected List<Skill> mySkills = new List<Skill>();
    protected List<Skill> skillsToUse = new List<Skill>();
    protected int currentSkillIndex = 0;

    [Header("floats")]
    protected float attackDistance;
    protected float distanceToPlayer;
    protected float timeBeforeAttack;
    protected float timeAfterAttack;

    [Header("Bools")]
    protected bool hasStopped = false;
    protected bool myStatInUI;
    
    // Use this for initialization
    protected virtual void Start()
    {
        InitializeStats();
        
        InitializeVariables();

        attackDistance = skillsToUse[currentSkillIndex].Range[skillsToUse[currentSkillIndex].level];

        agent.Warp(transform.position);

        Physics.IgnoreLayerCollision(0, 8);//Ignore collisions with the ground
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Do whateverEntity needs us to do
        base.Update();

        //Determine the distance to the player
        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        //Make sure our cooldown goes down
        skillsToUse[currentSkillIndex].CooldownManager(myStats);

        //Determine whether we should be moving towards the player of if we should try to use our skill
        StateHandler();
        
        //Move us, if we should move
        Movehandler();
    }

    //If active, move towards target
    protected virtual void Movehandler()
    {
        if (agent.isActiveAndEnabled)
        {
            if (!hasStopped && timeAfterAttack <= 0) //Only move if our agent is active
            {
                agent.speed = myStats.moveSpeedCurrent;
                agent.destination = target.transform.position;
            }
        }
    }

    //Determine if we should move or attack
    protected virtual void StateHandler()
    {
        timeAfterAttack -= Time.deltaTime;
        if (timeAfterAttack <= 0) //after an attack, do nothing untill we should act again
        {

            if (attackDistance >= distanceToPlayer) //within range, do not move, attack
            {
                if (agent.isActiveAndEnabled) //The first time this happens, make sure we don't move
                {
                    agent.destination = transform.position;
                    hasStopped = true;
                }
                
                transform.LookAt(target);

                //Determine if we have been in range for long enough
                timeBeforeAttack -= Time.deltaTime;
                if (timeBeforeAttack <= 0)
                {
                    UseSkill(skillsToUse[currentSkillIndex]);
                }
            }
            else // not within range, activate and move towards player
            {
                timeBeforeAttack = myStats.timeBeforeAttack; //reset this timer
                
                //Attempt to avoid "jump" when restarting movement, doesn't seem to work that good
                if (hasStopped)
                {
                    NavMeshHit _navMeshHit;
                    NavMesh.SamplePosition(transform.position, out _navMeshHit, 100f, NavMesh.AllAreas);

                    transform.position = _navMeshHit.position; 
                    hasStopped = false;
                }
                
            }
        }
    }
    

    protected override void OnDeath()
    {
        //If my stats are open, close them
        if (myStatInUI)
        {
            EnemyUI.Instance.Hide();
        }
        if (WaveSpawner.Instance != null)
        {
            WaveSpawner.Instance.xpGained += myStats.experienceForKill;
        }

        //REMOVE ME!!
        Destroy(gameObject);
    }

    protected virtual void OnMouseEnter()
    {
        EnemyUI.Instance.SetUpUnit(myStats);
        myStatInUI = true;
    }

    protected virtual void OnMouseExit()
    {
        EnemyUI.Instance.Hide();
        myStatInUI = false;
    }

    protected override void UseSkill(Skill skill)
    {
        //Can I use this skill?
        if (skill.AttemptCast(this))
        {
            //Use my skill
            skill.Action(target.position, this);
            timeAfterAttack = skillsToUse[currentSkillIndex].EnemyWindDown[0] + myStats.timeAfterAttack;

            //Give me a new skill. If I only have one it will always be the same
            SelectSkillToUse();
            attackDistance = skillsToUse[currentSkillIndex].Range[skillsToUse[currentSkillIndex].level];
        }
    }

    public override void InitializeStats()
    {
        myStats = Instantiate(myStatsPrefab);
        for (int i = 0; i < mySkills.Count; i++)
        {
            skillsToUse.Add(Instantiate(mySkills[i]));
        }
        
    }

    //Return a random skill from our available ones
    protected virtual void SelectSkillToUse(){

        currentSkillIndex = UnityEngine.Random.Range(0, skillsToUse.Count);
        
    }

    protected virtual void InitializeVariables()
    {
        //Get ALL of the components!
        agent = GetComponent<NavMeshAgent>();
        ownCollider = GetComponent<Collider>();

        //If, for some reason, we have anything other than Low avoidance quality, change it to Low
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;

        //Try to find our player
        target = Player.Instance.transform;

        //Set attack timers to our stats
        timeBeforeAttack = myStats.timeBeforeAttack;
        timeAfterAttack = myStats.timeAfterAttack;
        
        //Don't have windDown when it starts
        timeAfterAttack = 0;
    }
}
