using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class TrollBlood : Skill {

    [Header("Troll Blood")]
    [SerializeField] private float bonusHealthRegen;
    [SerializeField] private float slowPercentage;
    [SerializeField] private TrollBloodAura trollBloodAura;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        TrollBloodAura _tbAura = Instantiate(trollBloodAura);
        
        _tbAura.healthRegen = bonusHealthRegen;
        _tbAura.slowPercentage = slowPercentage;
        _tbAura.Duration = duration[level];

        caster.AddAura(_tbAura);


    }
    
}
