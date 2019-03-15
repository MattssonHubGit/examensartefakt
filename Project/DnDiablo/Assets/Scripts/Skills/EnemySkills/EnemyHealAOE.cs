using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyHealAOE : Skill {

    [Header("Skill Specifics")]
    [SerializeField] private float healAmountPerSecond;
    [SerializeField] private GameObject healGO;


    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _heal = Instantiate(healGO, caster.transform.position, Quaternion.identity, caster.transform);
        EnemyHealAOEBehaviour _EHB = _heal.GetComponent<EnemyHealAOEBehaviour>();
        _EHB.healAmount = healAmountPerSecond;
        _EHB.duration = duration[0];
        _EHB.caster = caster;
        
    }
}
