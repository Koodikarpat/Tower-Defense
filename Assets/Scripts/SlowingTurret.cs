﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTurret : TurretBase{

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        gameMaster = GameObject.Find("GameMaster");
    }

    void Update()
    {
        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
