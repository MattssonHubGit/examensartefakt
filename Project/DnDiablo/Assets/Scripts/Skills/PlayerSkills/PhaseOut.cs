using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class PhaseOut : Skill
{
    [Header("Skill specific")]
    [SerializeField] private PhaseOutAura auraPrefab;
    [SerializeField] private List<bool> enableCastingByLevel = new List<bool>();
    [SerializeField] private List<float> speedBoostByLevel = new List<float>();


    public override void Action(Vector3 targetPos, Entity caster)
    {

        PhaseOutAura _aura = Instantiate(auraPrefab);

        _aura.Duration = duration[level];
        _aura.enableCasting = enableCastingByLevel[level];
        _aura.speedBoost = speedBoostByLevel[level];

        caster.AddAura(_aura, caster);
    }
}
