using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{

    protected override void Awake()
    {
        base.Awake();
        jumpStrength = 10.0f;
        maxMoveSpeed = 8.0f;
        turnSmoothTime = 0.1f;
    }
}
