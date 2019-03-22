using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OneWayTeleport : MonoBehaviour {

    [SerializeField] public Transform endPoint;
    [HideInInspector] private Entity entityToMove;
    [HideInInspector] private NavMeshAgent agentToMove;
    [SerializeField] private bool isStartingTeleporter = true;

    public static OneWayTeleport InstancePart;

	// Use this for initialization
	void Awake () {
        if (InstancePart == null)
        {
            InstancePart = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Get our entity and its NavMeshAgent
        entityToMove = other.gameObject.GetComponent<Entity>();
        agentToMove = other.gameObject.GetComponent<NavMeshAgent>();

        if (entityToMove != null && agentToMove != null)
        {
            //Move our entity
            entityToMove.transform.position = endPoint.position;

            //Safety regulation to make sure we end on the NavMesh
            NavMeshHit _navMeshHit;
            NavMesh.SamplePosition(endPoint.position, out _navMeshHit, 100f, NavMesh.AllAreas);

            //Move the agent and remove previous directions
            agentToMove.Warp(endPoint.position);
            agentToMove.destination = entityToMove.transform.position;


            //If this is the starting teleporter, make the player able to use skills
            if (isStartingTeleporter)
            {
                entityToMove.canCast = true;

            }
        }

    }
}
