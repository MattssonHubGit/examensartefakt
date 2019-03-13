using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class BlockAura : Aura {
    public override void OnApply()
    {
        target.GetComponent<Collider>().enabled = false;
        target.GetComponent<MeshRenderer>().enabled = false;
        
    }

    public override void OnExpire()
    {
        target.GetComponent<Collider>().enabled = true;
        target.GetComponent<MeshRenderer>().enabled = true;
        target.EnableMovement();
    }

    public override void OnTick()
    {
        target.DisableMovement();
    }
}
