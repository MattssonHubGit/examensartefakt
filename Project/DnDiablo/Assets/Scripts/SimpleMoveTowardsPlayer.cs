using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleMoveTowardsPlayer : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    private float priority;
    private Rigidbody rb;
    private bool isWaiting = false;
    private Collider ownCollider;

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
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        ownCollider = GetComponent<Collider>();
        obstacle.enabled = false;
        rb = GetComponent<Rigidbody>();
        agent.Warp(transform.position);
        //priority = Random.Range(0, 10);

	}
	
	// Update is called once per frame
	void Update () {
        if (agent.isActiveAndEnabled)
        {
            agent.destination = target.transform.position;

        }
        priority = Vector3.Distance(target.position, transform.position);

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other == ownCollider)
        {
            Debug.Log("Enter Self");
        }
        SimpleMoveTowardsPlayer _s = other.GetComponent<SimpleMoveTowardsPlayer>();

        Debug.Log("Entered");
        if (_s != null)
        {
            float otherPriority = _s.Priority;

            if (priority < otherPriority)
            {
                //agent.enabled = false;
                //obstacle.enabled = true;
                //isWaiting = true;




            }

        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 _dir = this.transform.position - other.transform.position;
        if (other == ownCollider)
        {
            Debug.Log("Stay Self");
        }
        agent.destination = _dir;
        Debug.Log("Stay");
        //Rigidbody _orb = other.GetComponent<Rigidbody>();

        //if (_orb != null)
        //{
        //    //_orb.AddForce(_dir * 8000f, ForceMode.Impulse);
            
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == ownCollider)
        {
            Debug.Log("Exit Self");
        }

        if (isWaiting)
        {
            //obstacle.enabled = false;
            //agent.enabled = true;
            //isWaiting = false;
        }

    }

}
