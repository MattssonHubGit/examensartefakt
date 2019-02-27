using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class Fireball : Skill {

    [Header("Skill Specific")]
    [SerializeField] private GameObject spellObectPrefab;
    [SerializeField] private float moveSpeed;

    [SerializeField] private List<float> explosionDamageByLevel = new List<float>();
    [SerializeField] private List<float> explosionRadiusByLevel = new List<float>();

    public override void Action(Vector3 targetPos, Entity caster)
    {
        GameObject _objBall = Instantiate(spellObectPrefab, caster.transform.position, Quaternion.identity);
        FireballBehaviour _fireball = _objBall.GetComponent<FireballBehaviour>();

        _fireball.caster = caster;

        //Movement
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        _fireball.direction = _dir;
        _fireball.speed = moveSpeed;

        //Stats
        _fireball.damage = (explosionDamageByLevel[level] * caster.myStats.powerCurrent);
        _fireball.areaRadius = explosionRadiusByLevel[level];

        //Destroy object after duration is up
        Destroy(_objBall, duration[level]);
    }
}
