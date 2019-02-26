using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class PhaseOutAura : Aura
{
    public override void OnApply()
    {
        target.GetComponent<Collider>().enabled = false;
        target.GetComponent<MeshRenderer>().enabled = false;
    }

    public override void OnExpire()
    {
        target.GetComponent<Collider>().enabled = true;
        target.GetComponent<MeshRenderer>().enabled = true;
    }

    public override void OnTick()
    {

    }
}
