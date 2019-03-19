using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {

    [Header("Rotation - Y axis only")]
    [SerializeField] private float rotateSpeed;
    private int rotateDirection = 1;

    private Transform tf;

    private void Start()
    {
        tf = this.transform;

        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            rotateDirection = -1;
        }

    }

    // Update is called once per frame
    void Update ()
    {
        Rotation();
	}

    private void Rotation()
    {
        tf.eulerAngles += Vector3.up * rotateSpeed * Time.deltaTime;
    }


}
