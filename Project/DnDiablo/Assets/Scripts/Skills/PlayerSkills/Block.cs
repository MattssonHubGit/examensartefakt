using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class Block : Skill {

    [SerializeField] private BlockAura auraPrefab;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        BlockAura _aura = Instantiate(auraPrefab);

        _aura.Duration = duration[level];

        caster.AddAura(_aura);
    }
}
