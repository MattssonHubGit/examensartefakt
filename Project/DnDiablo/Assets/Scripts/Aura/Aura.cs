using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aura : ScriptableObject {

    [SerializeField] private float duration;
    [SerializeField] public Entity target;
    #region Getsetter
    public float Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }
    #endregion
    public abstract void OnApply();
    
    public abstract void OnTick();
    
    public abstract void OnExpire();
}
