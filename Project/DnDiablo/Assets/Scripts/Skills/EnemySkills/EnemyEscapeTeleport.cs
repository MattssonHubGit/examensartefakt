using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class EnemyEscapeTeleport : Skill {

    [Header("Skill specifics")]
    [SerializeField] private float teleportDistance;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();

        Vector3 _teleportPos = caster.transform.position - (_dir * teleportDistance);

        caster.gameObject.GetComponent<NavMeshAgent>().Warp(_teleportPos);

    }

    
}
