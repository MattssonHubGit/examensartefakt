using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : Entity {

    private Transform target;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    private float priority;
    private Rigidbody rb;
    private Collider ownCollider;
    private bool moveAway = false;
    private float attackDistance;
    private float distanceToPlayer;
    private bool hasStopped = false;
    private bool shouldStopForPriority = false;
    [SerializeField] private Skill mySkill;
    private Skill skillToUse;
    [SerializeField] private bool isTurret;
    private bool myStatInUI;

    public float Priority
    {
        get
        {
            return priority;
        }

        set
        {
            priority = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (skillToUse == null)
        {
            InitializeStats();
            attackDistance = skillToUse.Range[skillToUse.level];
        }
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        ownCollider = GetComponent<Collider>();
        if (!isTurret)
        {
            obstacle.enabled = false;
        }
        else
        {
            agent.enabled = false;
        }
        rb = GetComponent<Rigidbody>();
        agent.Warp(transform.position);
        Physics.IgnoreLayerCollision(0, 8);
        target = Player.Instance.transform;


    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        ManageHealth();
        ManageResource();
        mySkill.CooldownManager(myStats);
        StateHandler();
        
    }

    //If active, move towards target
    private void Movehandler()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.speed = myStats.moveSpeedCurrent;
            agent.destination = target.transform.position;
        }
    }

    //Determine if we should move or attack
    private void StateHandler()
    {

        if (attackDistance >= distanceToPlayer) //within range, do not move, attack
        {
            if (agent.isActiveAndEnabled)
            {
                agent.destination = transform.position;
            }
            if (!isTurret)
            {
                agent.enabled = false;
                //ownCollider.enabled = false;
                obstacle.enabled = true;
                hasStopped = true;
            }
            transform.LookAt(target);
            TryUseSkill(mySkill);
        }

        else // not within range, activate and move towards player
        {
            if (!isTurret) //Don't move if turret
            {
                obstacle.enabled = false;
                //ownCollider.enabled = true;
                agent.enabled = true;
                if (hasStopped)
                {
                    agent.Warp(transform.position); //Attempt to avoid "jump" when restarting movement
                    hasStopped = false;
                }
            }
        }
        
        Movehandler();
    }
    
    private void TryUseSkill(Skill skill)
    {
        if (skill.AttemptCast(this))
        {
            skill.Action(target.position, this);
        }
    }
    
    protected override void OnDeath()
    {
        if (myStatInUI)
        {
            EnemyUI.Instance.Hide();
        }
        if (WaveSpawner.Instance != null)
        {
            WaveSpawner.Instance.xpGained += myStats.experienceForKill;

        }
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        EnemyUI.Instance.SetUpUnit(myStats);
        myStatInUI = true;

    }

    private void OnMouseExit()
    {
        EnemyUI.Instance.Hide();
        myStatInUI = false;

    }

    protected override void UseSkill(Skill skill)
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeStats()
    {
        myStats = Instantiate(myStatsPrefab);
        skillToUse = Instantiate(mySkill);
        
    }
}
