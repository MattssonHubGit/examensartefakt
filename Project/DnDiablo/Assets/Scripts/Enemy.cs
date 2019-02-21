using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity {

    [SerializeField] private Transform target;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    private float priority;
    private Rigidbody rb;
    private Collider ownCollider;
    private bool moveAway = false;
    [SerializeField] private float attackDistance;
    private float distanceToPlayer;
    private bool hasStopped = false;
    private bool shouldStopForPriority = false;
    [SerializeField] private Skill mySkill;

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

        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        ownCollider = GetComponent<Collider>();
        obstacle.enabled = false;
        rb = GetComponent<Rigidbody>();
        agent.Warp(transform.position);
        Physics.IgnoreLayerCollision(0, 8);
        

    }

    // Update is called once per frame
    void Update()
    {



        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        mySkill.CooldownManager();

        if (attackDistance >= distanceToPlayer)
        {

            agent.destination = transform.position;
            agent.enabled = false;
            ownCollider.enabled = false;
            obstacle.enabled = true;
            hasStopped = true;
            Debug.Log(attackDistance + distanceToPlayer);
            TryUseSkill(mySkill);
        }
        
        else
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

        if (agent.isActiveAndEnabled)
        {
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
        throw new System.NotImplementedException();
    }
}
