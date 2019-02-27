using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ArrowSpray : Skill
{

    [Header("Spell Specific")]
    [SerializeField] private GameObject arrowPrefab;
    [Header("Movement")]
    [SerializeField] private List<float> speedByLevel = new List<float>();
    [SerializeField] [Range(3, 10)] private List<int> arrowsByLevel = new List<int>();
    [SerializeField] private float fireIntervalls;
    [SerializeField] private List<float> maxSpread = new List<float>();
    [Space]
    [SerializeField] private List<float> damageByLevel = new List<float>();
    

    public override void Action(Vector3 targetPos, Entity caster)
    {
        bool isEvenAmount = (arrowsByLevel[level] % 2 == 0); //Do we have a even amount of arrows?
        float angleOffSet = (arrowsByLevel[level] / maxSpread[level]); //Angle for arrows
        

        for (int i = 0; i < arrowsByLevel[level]; i++)
        {
            bool currentIsEven = (i % 2 == 0);

            //Calculate arrow angle offset based on amount of arrows, max spread, and current arrow created
            if (isEvenAmount)
            {

            }
            else
            {

            }

            GameObject _objArrow = Instantiate(arrowPrefab, caster.transform.position, Quaternion.identity);
            ArrowSprayBehaviour _scrArrow = _objArrow.GetComponent<ArrowSprayBehaviour>();

            //Movement
            Vector3 _dir = targetPos - caster.transform.position;
            _dir.Normalize();
            _scrArrow.direction = _dir;
            _scrArrow.speed = speedByLevel[level];
            _objArrow.transform.rotation = Quaternion.LookRotation(_dir);

            //Stats
            _scrArrow.damage = (damageByLevel[level] * caster.myStats.powerCurrent);

            //Destroy object after duration is up
            Destroy(_objArrow, duration[level]);
        }
    }
}
