using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class ArrowSpray : Skill
{

    [Header("Spell Specific")]
    [SerializeField] private GameObject arrowPrefab;
    [Header("Movement")]
    [SerializeField] private List<float> speedByLevel = new List<float>();
    [SerializeField] private List<int> arrowsByLevel = new List<int>();
    [SerializeField] private List<float> intervalSpeed = new List<float>();
    [SerializeField] private int intervals;
    [SerializeField] [Range(0, 1)] private List<float> maxSpread = new List<float>();
    [Space]
    [SerializeField] private List<float> damageByLevel = new List<float>();
    

    public override void Action(Vector3 targetPos, Entity caster)
    {
        caster.StartCoroutine(FireInterval(targetPos, caster));      
    }

    private IEnumerator FireInterval(Vector3 targetPos, Entity caster)
    {
        caster.DisableMovement();
        for (int i = 0; i < intervals; i++)
        {
            for (int j = 0; j < arrowsByLevel[level]; j++)
            {
                GameObject _objArrow = Instantiate(arrowPrefab, caster.transform.position, Quaternion.identity);
                ArrowSprayBehaviour _scrArrow = _objArrow.GetComponent<ArrowSprayBehaviour>();
                _scrArrow.caster = caster;

                //Movement
                Vector3 _dir = targetPos - caster.transform.position;
                _dir.Normalize();

                _dir = _dir + Random.insideUnitSphere * maxSpread[level]; //Random divergence for _dir
                _dir.Normalize();
                _scrArrow.direction = _dir;

                _objArrow.transform.rotation = Quaternion.LookRotation(_dir);
                caster.transform.rotation = Quaternion.LookRotation(_dir);

                //Useless animation
                if (caster.GetType() == typeof(Player))
                {
                    Player player = caster as Player;

                    if (player.MyAni != null)
                    {
                        player.MyAni.Play("Breath_Fw");
                    }
                }

                //Stats
                _scrArrow.damage = damageByLevel[level];
                _scrArrow.speed = speedByLevel[level];

                //Destroy object after duration is up
                Destroy(_objArrow, duration[level]);
            }
            yield return new WaitForSeconds(intervalSpeed[level]);
        }
        caster.EnableMovement();
    }
}
