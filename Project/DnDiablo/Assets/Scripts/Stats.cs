﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Stats : ScriptableObject {

    [Header("Health")]
	public float healthCurrent = 100;
    public float healthBase = 100;
    public float healthMax = 100;
    [Space]
    public float healthRegCurrent = 1;
    public float healthRegBase = 1;

    [Header("Power")]
    public float powerCurrent = 1;
    public float powerBase = 1;

    [Header("Resource")]
    public float resourceCurrent = 100;
    public float resourceBase = 100;
    public float resourceMax = 100;
    [Space]
    public float resourceRegCurrent = 1;
    public float resourceRegBase = 1;

    [Header("Cooldown reduction")]
    public float cooldownRedCurrent = 1;
    public float cooldownRedBase = 1;

    [Header("Movement speed")]
    public float moveSpeedCurrent = 1;
    public float moveSpeedBase = 1;
}
