using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{

    public override void Start()
    {
        base.Start();
        
        jumpStrength = 10.0f;
        moveSpeed = 10.0f;
        turnSmoothTime = 0.1f;
    }
}
