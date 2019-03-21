using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRemoveAfterTime : MonoBehaviour {

    [SerializeField] public float duration;
    
	// Update is called once per frame
	void Update () {

        duration -= Time.deltaTime;

        //Rermove this gameobject when its duration hits zero
        if (duration <= 0)
        {
            Destroy(this.gameObject);
        }

	}
}
