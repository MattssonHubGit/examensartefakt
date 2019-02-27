using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class DOTAura : Aura {

    [SerializeField] private float damagePerSecond;

    public override void OnApply()
    {
        
    }

    public override void OnExpire()
    {

    }

    public override void OnTick()
    {
        target.TakeDamage(damagePerSecond*Time.deltaTime);
    }
}
