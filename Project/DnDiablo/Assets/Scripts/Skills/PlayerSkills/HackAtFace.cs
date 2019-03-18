using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class HackAtFace : Skill {

    [SerializeField] private GameObject axeGO;
    [SerializeField] private List<float> damageByLevel = new List<float>();
    [SerializeField] private float swingSpeed;
    [SerializeField] [Range(1f, 3f)] private List<float> scaleByLevel = new List<float>();


    public override void Action(Vector3 targetPos, Entity caster)
    {

        GameObject _axe = Instantiate(axeGO, caster.transform.position, (caster.transform.rotation *= Quaternion.Euler(0, 0, 0)));
        _axe.transform.localScale *= scaleByLevel[level];

        HackAtFaceBehaviour _hack = _axe.GetComponent<HackAtFaceBehaviour>();

        caster.DisableMovement();

        _hack.caster = caster;
        _hack.damage = damageByLevel[level];
        _hack.duration = duration[level];
        _hack.rotationsSpeed = swingSpeed;

    }
}
