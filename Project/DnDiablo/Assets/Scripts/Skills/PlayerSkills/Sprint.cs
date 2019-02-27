using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]

public class Sprint : Skill
{
    [Header("Skill specific")]
    [SerializeField] private List<float> speedBonusPercentage = new List<float>();
    [SerializeField] private SprintAura sprintAura;



    public override void Action(Vector3 targetPos, Entity caster)
    {
        SprintAura _aura = Instantiate(sprintAura);
        _aura.speedBonusPercentage = speedBonusPercentage[level];
        _aura.Duration = duration[level];
        caster.AddAura(_aura);
    }
}
