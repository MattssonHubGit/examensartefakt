using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public static void Movement(Transform projectile, Vector3 direction, float speed)
    {
        projectile.position += direction * speed * Time.deltaTime;
    }
}
