using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stag : Animal
{

    private float chargeSpeed = 13.0f;
    public bool isCharging { private set; get; }
    public bool hasCharged { private set; get; }

    protected override void Awake()
    {
        base.Awake();
        hasCharged = false;
        isCharging = false;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleCharge();
    }

    void HandleCharge()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isOnGround && !hasCharged)
        {
            Charge();
        }
    }

    void Charge()
    {
        Vector3 chargeDirection = transform.forward;
        
        animalRb.AddForce(chargeDirection * chargeSpeed, ForceMode.Impulse);
        isCharging = true;
        hasCharged = true;

        StartCoroutine(ChargingTimer());
        StartCoroutine(WaitToCharge());
    }

    protected override void HandleWalk()
    {
        if(!isCharging)
        {
            base.HandleWalk();
        }
    }

    IEnumerator ChargingTimer()
    {
        yield return new WaitForSeconds(1);
        isCharging = false;
    }


    IEnumerator WaitToCharge()
    {
        yield return new WaitForSeconds(3);
        hasCharged = false;
    }

}
