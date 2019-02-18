using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public Stats myStats;

    public void ReduceResource(float amount)
    {
        myStats.resourceCurrent -= amount;
        if (myStats.resourceCurrent < 0)
        {
            myStats.resourceCurrent = 0;
        }
    }
}
