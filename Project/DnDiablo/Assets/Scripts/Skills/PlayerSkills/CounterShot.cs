using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterShot : Skill
{
    [Header("Skill specific")]
    [SerializeField] private CounterShotArua auraPrefab;
    [SerializeField] private GameObject arrowPrefab;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Stats")]
    [SerializeField] public List<float> damageMultiplierByLevel = new List<float>();


    public override void Action(Vector3 targetPos, Entity caster)
    {
        CounterShotArua _aura = Instantiate(auraPrefab);

        _aura.arrowPrefab = arrowPrefab;
        _aura.speed = speed;
        _aura.damageMultiplier = damageMultiplierByLevel[level];
        _aura.Duration = duration[level];

        caster.AddAura(_aura, caster);
    }
}
