using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleMoveTowardsPlayer : MonoBehaviour {

    [SerializeField] private Transform target;
    NavMeshAgent agent;
    private float priority;
    private Rigidbody rb;
    private Collider ownCollider;
    private bool moveAway = false;
    [SerializeField] private float attackDistance;
    private float distanceToPlayer;
    private bool hasStopped = false;
    private bool shouldStopForPriority = false;

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

    public float DistanceToPlayer
    {
        get
        {
            return distanceToPlayer;
        }

        set
        {
            distanceToPlayer = value;
        }
    }

    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        ownCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        agent.Warp(transform.position);
        //priority = Random.Range(0, 10);
        Physics.IgnoreLayerCollision(0, 8);

	}
	
	// Update is called once per frame
	void Update () {



        DistanceToPlayer = Vector3.Distance(transform.position, target.position);
        
        if (attackDistance >= DistanceToPlayer)
        {

            //agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
            //agent.destination = transform.position;
            agent.enabled = false;
            ownCollider.enabled = false;
            hasStopped = true;
        }
        //else if (shouldStopForPriority)
        //{
        //    //agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
        //    agent.destination = transform.position;

        //    agent.enabled = false;
        //    ownCollider.enabled = false;
        //    obstacle.enabled = true;
        //}
        else
        {
            //agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
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

    //IEnumerator StartMoving()
    //{

    //    yield return new WaitForSeconds(2);
    //    agent.enabled = true;
    //    if (hasStopped)
    //    {
    //        agent.Warp(transform.position);
    //        hasStopped = false;

    //    }

    //}


    private void OnTriggerEnter(Collider other)
    {
        //agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;


        SimpleMoveTowardsPlayer _s = other.GetComponent<SimpleMoveTowardsPlayer>();

        if (DistanceToPlayer > _s.distanceToPlayer)
        {
            shouldStopForPriority = true;
        }
        Debug.Log(distanceToPlayer + " " + _s.distanceToPlayer);

        //if (_s != null)
        //{

        //    if (distanceToPlayer < _s.distanceToPlayer)
        //    {
        //        moveAway = true;

        //    }
        //}
    }

    private void OnTriggerStay(Collider other)
    {

        //agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        //SimpleMoveTowardsPlayer _s = other.GetComponent<SimpleMoveTowardsPlayer>();
        //if (distanceToPlayer > _s.distanceToPlayer && agent.isActiveAndEnabled)
        //{
        //    agent.destination = transform.position;
        //    Vector3 _dir = transform.position - target.transform.position;
        //    agent.destination = _dir;
        //}

    }

    private void OnTriggerExit(Collider other)
    {
        //agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        //moveAway = false;
        if (shouldStopForPriority)
        {
            shouldStopForPriority = false;
        }

    }

}
