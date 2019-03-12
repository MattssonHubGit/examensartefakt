using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStab : Skill {

    [Header("Skill Specifics")]
    [SerializeField] private float damage;
    [SerializeField] private float stabSpeed;
    [SerializeField] private GameObject stabGO;


    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _stab = Instantiate(stabGO, caster.transform);
        EnemyStabBehaviour _ESB = _stab.GetComponent<EnemyStabBehaviour>();

        _ESB.damage = damage;
        _ESB.caster = caster;
        _ESB.stabSpeed = stabSpeed;
        _ESB.duration = duration[0];
        
    }
}
