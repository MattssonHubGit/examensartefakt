using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class Medicine : Skill  {

    [Header("Skill specific")]
    [SerializeField] private HOTAura auraPrefab;
    [Space]
    [SerializeField] private List<float> healthRegen = new List<float>();


    public override void Action(Vector3 targetPos, Entity caster)
    {
        HOTAura _aura = Instantiate(auraPrefab);

        _aura.healthRegen = healthRegen[level];
        _aura.Duration = duration[level];

        caster.AddAura(_aura);
    }
}
