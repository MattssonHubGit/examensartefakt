using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Fireball : Skill {

    [Header("Skill Specific")]
    [SerializeField] private GameObject spellObectPrefab;
    [SerializeField] private float moveSpeed;


    public override void Action(Vector3 targetPos, Entity caster)
    {
        //Debug.Log("Fireball::Action -- Skill logic not implemented");

        GameObject _objBall = Instantiate(spellObectPrefab, caster.transform.position, Quaternion.identity);
        Projectile _projectile = _objBall.GetComponent<Projectile>();

        Vector3 _dir = targetPos - caster.transform.position;
        _dir.Normalize();
        _projectile.direction = _dir;
        _projectile.speed = moveSpeed;
    }
}
