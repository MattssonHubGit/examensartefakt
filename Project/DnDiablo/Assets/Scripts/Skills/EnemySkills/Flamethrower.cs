using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flamethrower : Skill {

    [Header("Spell Specific")]
    [SerializeField] private GameObject firePrefab;
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private int flameAmount;
    [SerializeField] private float intervalSpeed;
    [SerializeField] private int intervals;
    [SerializeField] [Range(0, 1)] private float maxSpread;
    [Space]
    [SerializeField] private float damage;
    [SerializeField] private float rotationSpeed;


    public override void Action(Vector3 targetPos, Entity caster)
    {
        caster.StartCoroutine(FireInterval(targetPos, caster));
    }

    private IEnumerator FireInterval(Vector3 targetPos, Entity caster)
    {
        for (int i = 0; i < intervals; i++)
        {
            for (int j = 0; j < flameAmount; j++)
            {
                GameObject _objFlame = Instantiate(firePrefab, caster.transform.position, Quaternion.identity);
                FlamethrowerBehaviour _FB = _objFlame.GetComponent<FlamethrowerBehaviour>();
                _FB.caster = caster;


                targetPos = caster.GetComponent<Enemy>().target.position - caster.transform.position;
                float step = rotationSpeed * Time.deltaTime;

                //Movement
                Vector3 _dir = Vector3.RotateTowards(caster.transform.forward, targetPos, step, 0.0f);
                _dir.Normalize();

                caster.transform.rotation = Quaternion.LookRotation(_dir);

                _dir = _dir + Random.insideUnitSphere * maxSpread; //Random divergence for _dir
                _dir.Normalize();
                _FB.direction = _dir;

                _objFlame.transform.rotation = Quaternion.LookRotation(_dir);
                
                //Stats
                _FB.damage = (damage * caster.myStats.powerCurrent);
                _FB.speed = speed;

                //Destroy object after duration is up
                Destroy(_objFlame, duration[level]);
            }
            yield return new WaitForSeconds(intervalSpeed);
        }
    }
}
