using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class Medicine : Skill  {

    [Header("Skill specific")]

    [Header("Regen %")]
    [SerializeField] private HOTAura auraRegenPrefab;
    [Space]
    [SerializeField] private List<float> healthRegen = new List<float>();

    [Header("Stat Increase")]
    [SerializeField] private List<bool> increaseStatsByLevel = new List<bool>();
    [SerializeField] private StatIncreaseAura auraStatIncreasePrefab;
    [SerializeField] private List<StatIncreaseAura.StatTypes> typesToIncrease = new List<StatIncreaseAura.StatTypes>();
    [SerializeField] [Range(0, 15)] private int amountToIncreaseWith;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        HOTAura _aura = Instantiate(auraRegenPrefab);

        _aura.healthRegen = healthRegen[level];
        _aura.Duration = duration[level];

        caster.AddAura(_aura, caster);

        if (increaseStatsByLevel[level])
        {
            for (int i = 0; i < typesToIncrease.Count; i++)
            {
                StatIncreaseAura _statAura = Instantiate(auraStatIncreasePrefab);

                _statAura.statToIncrease = typesToIncrease[i];
                _statAura.amount = amountToIncreaseWith;
                _statAura.Duration = duration[level];

                caster.AddAura(_statAura, caster);
            }
        }

    }
}
