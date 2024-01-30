using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    private float fastSpeed = 13;


    protected override void Start()
    {
        base.Start();

        speed = fastSpeed;
    }
}
