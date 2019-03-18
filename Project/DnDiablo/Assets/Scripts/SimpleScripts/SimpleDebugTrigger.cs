using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDebugTrigger : MonoBehaviour {

	private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
    }
}
