using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[CreateAssetMenu()]
public class Blink : Skill
{
    [SerializeField] private StatIncreaseAura statAura;
    [SerializeField] private List<int> powerIncreasesByLevel = new List<int>();
    [SerializeField] private List<float> auraDurationByLevel = new List<float>();
    [SerializeField] private List<bool> increasePowerByLevel = new List<bool>();

    public override void Action(Vector3 targetPos, Entity caster)
    {
        //If targetPos is within range, just go there
        float dist = Vector3.Distance(caster.transform.position, targetPos);
        if (dist <= range[level])
        {
            caster.gameObject.GetComponent<NavMeshAgent>().Warp(targetPos);
            return;
        }

        //Otherwise teleport as far as possible
        Vector3 dir = targetPos - caster.transform.position;
        dir.Normalize();
        Vector3 _inRangeTarget = caster.transform.position + (dir * range[level]);

        caster.gameObject.GetComponent<NavMeshAgent>().Warp(_inRangeTarget);

        //Check if our power shloud be increased
        if (increasePowerByLevel[level])
        {
            //Increase our power for a while
            StatIncreaseAura _aura = Instantiate(statAura);

            _aura.amount = powerIncreasesByLevel[level];
            _aura.statToIncrease = StatIncreaseAura.StatTypes.POWER;
            _aura.Duration = auraDurationByLevel[level];

            caster.AddAura(_aura, caster);
        }
    }
}
