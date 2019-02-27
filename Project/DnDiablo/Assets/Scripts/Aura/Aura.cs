using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aura : ScriptableObject {

    [SerializeField] private float duration;
    [HideInInspector] public Entity target;
    [HideInInspector] public Entity applier;
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
