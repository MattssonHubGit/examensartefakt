using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class BlockAura : Aura {
    public override void OnApply()
    {
        target.GetComponent<Collider>().enabled = false;
        
    }

    public override void OnExpire()
    {
        target.GetComponent<Collider>().enabled = true;
        target.EnableMovement();
    }

    public override void OnTick()
    {
        target.DisableMovement();
    }
}
