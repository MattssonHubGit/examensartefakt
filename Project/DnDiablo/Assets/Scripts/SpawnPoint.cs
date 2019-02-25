using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public static SpawnPoint Instance;


	// Use this for initialization
	void Start () {
        #region SingleTon
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion



    }

    // Update is called once per frame
    void Update () {
		
	}
}
