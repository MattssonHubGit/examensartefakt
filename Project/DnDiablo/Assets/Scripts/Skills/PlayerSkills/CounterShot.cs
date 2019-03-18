using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class CounterShot : Skill
{
    [Header("Skill specific")]
    [SerializeField] private CounterShotArua auraPrefab;
    [SerializeField] private GameObject arrowPrefab;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Stats")]
    [SerializeField] public List<float> damageMultiplierByLevel = new List<float>();
    [SerializeField] [Range(1, 15)] private List<int> stacksByLevel = new List<int>();


    public override void Action(Vector3 targetPos, Entity caster)
    {
        for (int i = 0; i < stacksByLevel[level]; i++)
        {
            CounterShotArua _aura = Instantiate(auraPrefab);

            _aura.arrowPrefab = arrowPrefab;
            _aura.speed = speed;
            _aura.damageMultiplier = damageMultiplierByLevel[level];
            _aura.Duration = duration[level];

            caster.AddAura(_aura, caster);
        }

    }
}
