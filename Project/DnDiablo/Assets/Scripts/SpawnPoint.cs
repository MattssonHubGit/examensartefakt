using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public static SpawnPoint Pseuedo;


	// Use this for initialization
	void Awake () {
      /*  #region SingleTon
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion*/

        Pseuedo = this;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
