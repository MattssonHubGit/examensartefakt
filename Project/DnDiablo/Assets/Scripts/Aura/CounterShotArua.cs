using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterShotArua : CounterAura
{
    [HideInInspector] public GameObject arrowPrefab;

    [Header("Movement")]
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 direction;

    [Header("Stats")]
    [HideInInspector] public float damageMultiplier;
    [HideInInspector] public float duration;

    public override void Counter(Entity caster, Entity target, float damageTaken)
    {
        GameObject _objArrow = Instantiate(arrowPrefab, caster.transform.position, Quaternion.identity);
        CounterShotBehaviour _scrArrow = _objArrow.GetComponent<CounterShotBehaviour>();
        _scrArrow.caster = caster;

        //Movement
        _scrArrow.target = target;
        _scrArrow.speed =  speed;


        //Stats
        _scrArrow.damage = damageMultiplier * (caster.myStats.powerCurrent * damageMultiplier);

        //Remove Aura from caster
        target.RemoveAura(this);


        //Destroy object after duration is up              
        Destroy(_objArrow, duration);
    }

}
