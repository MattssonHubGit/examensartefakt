using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : Entity {

    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private Rigidbody rb;
    private Collider ownCollider;
    private Vector3 stopPos;
    private bool moveAway = false;
    private float attackDistance;
    private float distanceToPlayer;
    private bool hasStopped = false;
    private bool shouldStopForPriority = false;
    [SerializeField] private Skill mySkill;
    private Skill skillToUse;
    [SerializeField] private bool isTurret;
    private bool myStatInUI;
    [SerializeField] private float timeBeforeAttack;
    [SerializeField] private float timeAfterAttack;
    private float beforeAttackTimeOrigin;
    private float afterAttackTimeOrigin;



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
        beforeAttackTimeOrigin = timeBeforeAttack;
        afterAttackTimeOrigin = timeAfterAttack;
        timeAfterAttack = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
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

        timeAfterAttack -= Time.deltaTime;

        if (attackDistance >= distanceToPlayer) //within range, do not move, attack
        {

            stopPos = transform.position;
            if (agent.isActiveAndEnabled)
            {
                agent.destination = transform.position;
            }
            if (!isTurret)
            {
                agent.enabled = false;
                obstacle.enabled = true;
                hasStopped = true;
            }
            transform.LookAt(target);

            timeBeforeAttack -= Time.deltaTime;
            TryUseSkill(mySkill);
        }

        else // not within range, activate and move towards player
        {
            timeBeforeAttack = beforeAttackTimeOrigin;
            if (!isTurret && timeAfterAttack <= 0) //Don't move if turret
            {
                obstacle.enabled = false;
                agent.enabled = true;
                if (hasStopped)
                {
                    agent.Warp(stopPos); //Attempt to avoid "jump" when restarting movement
                    hasStopped = false;
                }
            }
        }
        
        Movehandler();
    }
    
    private void TryUseSkill(Skill skill)
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
