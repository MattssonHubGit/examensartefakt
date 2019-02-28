using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : Entity {

    [Header("initialization")]
    protected NavMeshAgent agent;
    protected NavMeshObstacle obstacle;
    protected Collider ownCollider;
    protected Transform target;
    [SerializeField] protected Skill mySkill;
    protected Skill skillToUse;

    [Header("floats")]
    protected float attackDistance;
    protected float distanceToPlayer;
    protected float timeBeforeAttack;
    protected float timeAfterAttack;
    protected float beforeAttackTimeOrigin;
    protected float afterAttackTimeOrigin;

    [Header("Bools")]
    protected bool hasStopped = false;
    protected bool myStatInUI;
    

    // Use this for initialization
    void Start()
    {
        if (skillToUse == null)
        {
            InitializeStats();
            attackDistance = skillToUse.Range[skillToUse.level];
        }

        InitializeVariables();

        agent.Warp(transform.position);

        Physics.IgnoreLayerCollision(0, 8);//Ignore collisions with the ground
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        mySkill.CooldownManager(myStats);
        StateHandler();
        Movehandler();
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
        timeAfterAttack -= Time.deltaTime;

        if (attackDistance >= distanceToPlayer) //within range, do not move, attack
        {
            if (agent.isActiveAndEnabled)
            {
                agent.destination = transform.position;
            }
            
            agent.enabled = false;
            obstacle.enabled = true;
            hasStopped = true;
            
            transform.LookAt(target);

            timeBeforeAttack -= Time.deltaTime;
            UseSkill(mySkill);
        }
        else // not within range, activate and move towards player
        {
            timeBeforeAttack = beforeAttackTimeOrigin;
            if (timeAfterAttack <= 0) 
            {
                obstacle.enabled = false;
                agent.enabled = true;
                if (hasStopped)
                {
                    NavMeshHit _navMeshHit;
                    NavMesh.SamplePosition(transform.position, out _navMeshHit, 100f, NavMesh.AllAreas);

                    transform.position = _navMeshHit.position; //Attempt to avoid "jump" when restarting movement
                    hasStopped = false;
                }
            }
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
        if (timeBeforeAttack <= 0)
        {
            if (skill.AttemptCast(this))
            {
                skill.Action(target.position, this);
                timeAfterAttack = skill.Duration[0] + afterAttackTimeOrigin;
            }
        }
    }

    public override void InitializeStats()
    {
        myStats = Instantiate(myStatsPrefab);
        skillToUse = Instantiate(mySkill);
    }

    protected virtual void InitializeVariables()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        ownCollider = GetComponent<Collider>();
        target = Player.Instance.transform;
        beforeAttackTimeOrigin = timeBeforeAttack;
        afterAttackTimeOrigin = timeAfterAttack;
        timeAfterAttack = 0;
    }
}
