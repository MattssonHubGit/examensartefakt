using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AxeThrow : Skill
{
    [Header("Spell Specific")]
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private List<float> speedByLevel = new List<float>();
    [SerializeField] private List<bool> penetrateByLevel = new List<bool>();


    public override void Action(Vector3 targetPos, Entity caster)
    {
        Debug.Log("AxeThrow::Action() -- Skill logic not implemented yet.");
    }
}
