using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFindMeProgressBar : MonoBehaviour {

    public static SimpleFindMeProgressBar PartInstance;

    private void Awake()
    {
        if (PartInstance == null)
        {
            PartInstance = this;
        }
        PartInstance.gameObject.SetActive(false);
    }
}
