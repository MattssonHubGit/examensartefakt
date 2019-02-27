using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class AxeThrow : Skill
{
    [Header("Spell Specific")]
    [SerializeField] private GameObject axePrefab;
    [Space]
    [SerializeField] private List<float> speedByLevel = new List<float>();
    [SerializeField] private List<bool> penetrateByLevel = new List<bool>();
    [Space]
    [SerializeField] private List<float> damageByLevel = new List<float>();


    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _objAxe = Instantiate(axePrefab, caster.transform.position, Quaternion.identity);
        AxeThrowBehaviour _scrAxe = _objAxe.GetComponent<AxeThrowBehaviour>();
        _scrAxe.caster = caster;

        //Movement
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        _scrAxe.direction = _dir;
        _scrAxe.speed = speedByLevel[level];
        _scrAxe.penetrate = penetrateByLevel[level];
        _objAxe.transform.rotation = Quaternion.LookRotation(_dir);

        //Stats
        _scrAxe.damage = (damageByLevel[level] * caster.myStats.powerCurrent);

        //Destroy object after duration is up
        Destroy(_objAxe, duration[level]);
    }
}
