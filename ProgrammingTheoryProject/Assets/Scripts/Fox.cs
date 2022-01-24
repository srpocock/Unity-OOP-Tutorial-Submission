using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Fox : Animal
{

    // POLYMORPHISM
    protected override void Awake()
    {
        base.Awake();
        jumpStrength = 10.0f;
        maxMoveSpeed = 8.0f;
        turnSmoothTime = 0.1f;
    }
}
