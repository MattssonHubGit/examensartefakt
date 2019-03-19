using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class TrollBlood : Skill {

    [Header("Troll Blood")]
    [SerializeField] private List<float> bonusHealthRegenByLevel = new List<float>();
    [SerializeField] [Range(0f, 1f)] private List<float> slowPercentageByLevel = new List<float>();
    [SerializeField] private TrollBloodAura trollBloodAura;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        TrollBloodAura _tbAura = Instantiate(trollBloodAura);
        
        _tbAura.healthRegen = bonusHealthRegenByLevel[level];
        _tbAura.slowPercentage = slowPercentageByLevel[level];
        _tbAura.Duration = duration[level];

        caster.AddAura(_tbAura, caster);


    }
    
}
