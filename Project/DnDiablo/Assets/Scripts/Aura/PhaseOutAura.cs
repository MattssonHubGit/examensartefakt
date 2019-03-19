using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class PhaseOutAura : Aura
{
    [HideInInspector] public bool enableCasting;
    [HideInInspector] public float speedBoost;
    [HideInInspector] private float speedChange;
    public override void OnApply()
    {
        //Initialization
        target.GetComponent<Collider>().enabled = false;
        Player.Instance.GFXDefault.SetActive(false);
        Player.Instance.GFXTransparent.SetActive(true);

        //Check if we are allowed to use skills during the duration
        if (!enableCasting)
        {
            target.canCast = false;

        }

        //Give us more speed
        speedChange = (target.myStats.moveSpeedCurrent * speedBoost);
        target.myStats.moveSpeedCurrent += speedChange;
    }

    public override void OnExpire()
    {
        //Reverse everything
        target.GetComponent<Collider>().enabled = true;
        Player.Instance.GFXDefault.SetActive(true);
        Player.Instance.GFXTransparent.SetActive(false);
        target.canCast = true;

        target.myStats.moveSpeedCurrent -= speedChange;
    }

    public override void OnTick()
    {

    }
}
