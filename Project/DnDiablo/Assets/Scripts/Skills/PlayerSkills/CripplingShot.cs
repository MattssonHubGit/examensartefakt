using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class CripplingShot : Skill
{
    [Header("Spell Specific")]
    [SerializeField] private GameObject arrowPrefab;
    [Header("Movement")]
    [SerializeField] private List<float> speedByLevel = new List<float>();
    [Header("Slow")]
    [SerializeField] private List<bool> slowByLevel = new List<bool>();
    [SerializeField] [Range(0f, 3f)] private List<float> slowPercantageByLevel = new List<float>();
    [SerializeField] private List<float> slowDurationByLevel = new List<float>();
    [Header("Damage")]
    [SerializeField] private List<float> damageByLevel = new List<float>();

    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _objArrow = Instantiate(arrowPrefab, caster.transform.position, Quaternion.identity);
        CripplingShotBehaviour _scrArrow = _objArrow.GetComponent<CripplingShotBehaviour>();
        _scrArrow.caster = caster;

        //Movement
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        _scrArrow.direction = _dir;
        _scrArrow.speed = speedByLevel[level];
        _objArrow.transform.rotation = Quaternion.LookRotation(_dir);

        //Stats
        _scrArrow.damage = damageByLevel[level];
        _scrArrow.slow = slowByLevel[level];
        _scrArrow.slowDuration = slowDurationByLevel[level];
        _scrArrow.slowPercentage = slowPercantageByLevel[level];


        //Destroy object after duration is up
        Destroy(_objArrow, duration[level]);
    }
}
