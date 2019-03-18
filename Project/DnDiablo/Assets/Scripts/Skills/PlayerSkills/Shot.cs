using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class Shot : Skill
{
    [Header("Spell Specific")]
    [SerializeField] private GameObject arrowPrefab;
    [Space]
    [SerializeField] private List<float> speedByLevel = new List<float>();
    [SerializeField] private List<bool> penetrateByLevel = new List<bool>();
    [Space]
    [SerializeField] private List<float> damageByLevel = new List<float>();

    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _objArrow = Instantiate(arrowPrefab, caster.transform.position, Quaternion.identity);
        ShotBehaviour _scrArrow = _objArrow.GetComponent<ShotBehaviour>();
        _scrArrow.caster = caster;

        //Movement
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        _scrArrow.direction = _dir;
        _scrArrow.speed = speedByLevel[level];
        _scrArrow.penetrate = penetrateByLevel[level];
        _objArrow.transform.rotation = Quaternion.LookRotation(_dir);


        //Stats
        _scrArrow.damage = damageByLevel[level];

        //Destroy object after duration is up
        Destroy(_objArrow, duration[level]);
    }
}
