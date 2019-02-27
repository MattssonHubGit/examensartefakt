using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class ArcaneSphere : Skill
{
    [Header("Spell Specific")]
    [SerializeField] private GameObject spherePrefab;
    [Space]
    [SerializeField] private List<float> damagePerSecByLevel = new List<float>();
    [Space]
    [SerializeField] [Range(0, 3)] private List<float> radiusPercantageByLevel = new List<float>();

    public override void Action(Vector3 targetPos, Entity caster)
    {
        //If targetPos is within range, just place it there - otherwise go as far as possible
        Vector3 _trueTargetPos = targetPos;

        float dist = Vector3.Distance(caster.transform.position, targetPos);
        if (dist >= range[level])
        {
            Vector3 dir = targetPos - caster.transform.position;
            dir.Normalize();
            _trueTargetPos = caster.transform.position + (dir * range[level]);
        }

        //Create Object
        GameObject _objSphere = Instantiate(spherePrefab, _trueTargetPos, Quaternion.identity);
        ArcaneSphereBehaviour _scrSphere = _objSphere.GetComponent<ArcaneSphereBehaviour>();
        _scrSphere.caster = caster;

        //Size
        _objSphere.transform.localScale *= radiusPercantageByLevel[level];

        //Stats
        _scrSphere.damagePerSec = (damagePerSecByLevel[level] * caster.myStats.powerCurrent);

        //Destroy object after duration is up
        Destroy(_objSphere, duration[level]);
    }
}
