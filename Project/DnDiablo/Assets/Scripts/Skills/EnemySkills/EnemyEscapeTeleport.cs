using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class EnemyEscapeTeleport : Skill {

    [Header("Skill specifics")]
    [SerializeField] private float teleportDistance;
    [SerializeField] private GameObject teleportEffects;
    [SerializeField] private float effectsDuration;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        
        GameObject _effects = Instantiate(teleportEffects, caster.transform.position, Quaternion.identity);

        Vector3 _teleportPos = caster.transform.position - (_dir * teleportDistance);

        caster.gameObject.GetComponent<NavMeshAgent>().Warp(_teleportPos);

        Destroy(_effects, effectsDuration);
    }

    
}
