using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class BlockAura : Aura {


    public override void OnApply()
    {
        
    }

    public override void OnExpire()
    {
        target.canTakeDamage = true;
    }

    public override void OnTick()
    {
        target.canTakeDamage = false;
    }
}
