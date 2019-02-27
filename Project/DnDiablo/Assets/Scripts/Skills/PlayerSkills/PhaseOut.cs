using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class PhaseOut : Skill
{
    [Header("Skill specific")]
    [SerializeField] private PhaseOutAura auraPrefab;


    public override void Action(Vector3 targetPos, Entity caster)
    {

        PhaseOutAura _aura = Instantiate(auraPrefab);

        _aura.Duration = duration[level];

        caster.AddAura(_aura, caster);
    }
}
