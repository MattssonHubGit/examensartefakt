using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleActivateObstacle : MonoBehaviour {

    NavMeshObstacle obstacle;

	// Use this for initialization
	void Start () {
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(ActivateObstacle());

	}


    IEnumerator ActivateObstacle()
    {

        yield return new WaitForSeconds(1);
        
        obstacle.enabled = true;
        
    }
}
