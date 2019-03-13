using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyShot : Skill {

    [Header("Spell Specific")]
    [SerializeField] private GameObject arrowPrefab;
    [Space]
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private float damage;

    public override void Action(Vector3 targetPos, Entity caster)
    {
        
        GameObject _objArrow = Instantiate(arrowPrefab, caster.transform.position, Quaternion.identity);
        EnemyShotBehaviour _scrArrow = _objArrow.GetComponent<EnemyShotBehaviour>();
        _scrArrow.caster = caster;

        //Movement
        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        _scrArrow.direction = _dir;
        _scrArrow.speed = speed;
        _objArrow.transform.rotation = Quaternion.LookRotation(_dir);
        
        //Stats
        _scrArrow.damage = damage;

        //Destroy object after duration is up
        Destroy(_objArrow, duration[level]);
    }
}
