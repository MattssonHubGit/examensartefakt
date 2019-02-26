using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class Heal : Skill
{
    [Header("Skill Specific")]
    [SerializeField] private List<float> healByLevel = new List<float>();

    public override void Action(Vector3 targetPos, Entity caster)
    {
        caster.TakeDamage(-1f * (healByLevel[level] * caster.myStats.powerCurrent));
    }
}
