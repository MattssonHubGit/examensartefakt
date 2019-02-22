using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            attackDistance = skillToUse.Range;
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

        ManageHealth();
        ManageResource();

        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        mySkill.CooldownManager(myStats);

        if (attackDistance >= distanceToPlayer)
        {
            if (agent.isActiveAndEnabled)
            {
                agent.destination = transform.position;
            }
            if (!isTurret)
            {
                agent.enabled = false;
                ownCollider.enabled = false;
                obstacle.enabled = true;
                hasStopped = true;
            }
            TryUseSkill(mySkill);
        }
        
        else
        {
            if (!isTurret)
            {
                obstacle.enabled = false;
                ownCollider.enabled = true;
                agent.enabled = true;
                if (hasStopped)
                {
                    agent.Warp(transform.position);
                    hasStopped = false;
                }
            }
        }

        if (agent.isActiveAndEnabled)
        {
            agent.speed = myStats.moveSpeedCurrent;
            agent.destination = target.transform.position;
        }
    }
    
    private void TryUseSkill(Skill skill)
    {
        if (skill.AttemptCast(this))
        {
            skill.Action(target.position, this);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
    }

    protected override void OnDeath()
    {
        Destroy(gameObject);
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
