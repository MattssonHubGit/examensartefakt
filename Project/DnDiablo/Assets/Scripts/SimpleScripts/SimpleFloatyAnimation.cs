using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFloatyAnimation : MonoBehaviour {

    [Header("Movement - Y axis only")]
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatUpMax;
    [SerializeField] private float floatDownMax;
    private bool moveUp = true;
    private Vector3 floatMax;
    private Vector3 floatMin;
    private Vector3 velocityRef;

    [Header("Rotation - Y axis only")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool rotate;
    [SerializeField] private bool startAtRandom;
    private int rotateDirection = 1;

    private Transform tf;

    private void Awake()
    {
        tf = this.transform;
        floatMax = new Vector3(tf.position.x, tf.position.y + floatUpMax, tf.position.z);
        floatMin = new Vector3(tf.position.x, tf.position.y - floatUpMax, tf.position.z);

        if (startAtRandom)
        {
            tf.eulerAngles = new Vector3(tf.eulerAngles.x, Random.Range(0f, 360f), tf.eulerAngles.z);
        }
    }

    private void Update()
    {
        Movement();
        if (rotate == true)
        {
            Rotation();
        }
    }

    private void Movement()
    {

        if (moveUp == true)
        {
            tf.position = Vector3.SmoothDamp(tf.position, floatMax + Vector3.up*floatUpMax, ref velocityRef, floatSpeed);
        }
        if (moveUp == false)
        {
            tf.position = Vector3.SmoothDamp(tf.position, floatMin - Vector3.up * floatDownMax, ref velocityRef, floatSpeed);
        }

        //Switch direction
        if (tf.position.y >= floatMax.y)
        {
            moveUp = false;
        }
        if (tf.position.y <= floatMin.y)
        {
            moveUp = true;
        }

    }

    private void Rotation()
    {
        tf.eulerAngles += Vector3.up * rotateSpeed * Time.deltaTime;
    }

}
