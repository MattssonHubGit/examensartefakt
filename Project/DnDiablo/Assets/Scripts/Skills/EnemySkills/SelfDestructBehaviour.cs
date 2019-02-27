using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructBehaviour : SpellBehaviour
{

    public float damage;
    public float duration;
    public Vector3 expansionRate;


	// Use this for initialization
	void Start () {
        gameObject.transform.localScale = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.localScale += (expansionRate * Time.deltaTime);
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        
        IDamageable _damagable = other.gameObject.GetComponent<IDamageable>();

        if (_damagable != null)
        {
            _damagable.TakeDamage(damage, caster);
        }

        CameraController.Instance.AddShake(damage);
        
    }
}
