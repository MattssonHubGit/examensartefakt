using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class IceLance : Skill
{
    [Header("Spell Specific")]
    [SerializeField] private GameObject lancePrefab;
    [Header("Movement")]
    [SerializeField] private List<float> speedByLevel = new List<float>();
    [SerializeField] private List<bool> penetrateByLevel = new List<bool>();
    [Header("Slow")]
    [SerializeField] private List<bool> slowByLevel = new List<bool>();
    [SerializeField] [Range(0f, 1f)] private List<float> slowPercantageByLevel = new List<float>();
    [SerializeField] private List<float> slowDurationByLevel = new List<float>();
    [Header("Damage")]
    [SerializeField] private List<float> damageByLevel = new List<float>();

    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _objLance = Instantiate(lancePrefab, caster.transform.position, Quaternion.identity);
        IceLanceBehaviour _scrLance = _objLance.GetComponent<IceLanceBehaviour>();
        _scrLance.caster = caster;


        //Movement
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        _scrLance.direction = _dir;
        _scrLance.speed = speedByLevel[level];
        _scrLance.penetrate = penetrateByLevel[level];
        _objLance.transform.rotation = Quaternion.LookRotation(_dir);

        //Stats
        _scrLance.damage = (damageByLevel[level] * caster.myStats.powerCurrent);
        _scrLance.slow = slowByLevel[level];
        _scrLance.slowDuration = slowDurationByLevel[level];
        _scrLance.slowPercentage = slowPercantageByLevel[level];


        //Destroy object after duration is up
        Destroy(_objLance, duration[level]);
    }
}
