using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[CreateAssetMenu]
public class EnemyChargeAura : Aura {

    public Entity caster;
    public Vector3 endPos;
    public float chargeSpeed;
    public float damage;
    private NavMeshAgent agent;
    [SerializeField] private GameObject chargeGO;
    private GameObject chargeDestruktionGO;

    public override void OnApply()
    {
        agent = caster.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        GameObject _chargeGO = Instantiate(chargeGO, caster.transform);
        EnemyChargeBehaviour _ECB = _chargeGO.GetComponent<EnemyChargeBehaviour>();
        _ECB.damage = damage;
        _ECB.caster = caster;
        _ECB.auraToRemove = this;
        chargeDestruktionGO = _chargeGO;
    }

    public override void OnExpire()
    {

        agent.enabled = true;
        NavMeshHit _navMeshHit;
        NavMesh.SamplePosition(caster.transform.position, out _navMeshHit, 100f, NavMesh.AllAreas);

        agent.Warp(_navMeshHit.position);

        Destroy(chargeDestruktionGO);
    }

    public override void OnTick()
    {
        agent.enabled = false;
        if (caster.transform.position == endPos)
        {
            caster.RemoveAura(this);
            return;
        }
        endPos.y = caster.transform.position.y;
        caster.transform.position = Vector3.MoveTowards(caster.transform.position, endPos, chargeSpeed * Time.deltaTime);
        
        
    }
}
