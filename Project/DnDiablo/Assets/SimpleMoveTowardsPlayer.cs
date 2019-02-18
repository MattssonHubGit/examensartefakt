using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleMoveTowardsPlayer : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        agent.Warp(transform.position);

	}
	
	// Update is called once per frame
	void Update () {

        agent.destination = target.transform.position;


    }
}
