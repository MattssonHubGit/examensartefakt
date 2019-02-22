using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Base Projectile")]
    [SerializeField] public float speed;
    [SerializeField] public Vector3 direction = new Vector3();

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
