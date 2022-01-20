using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stag : Animal
{

    private float chargeSpeed = 13.0f;
    private bool hasCharged = false;

    // Update is called once per frame
    public override void FixedUpdate()
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
        hasCharged = true;

        StartCoroutine(WaitToCharge());
        
    }

    IEnumerator WaitToCharge()
    {
        yield return new WaitForSeconds(3);
        hasCharged = false;
    }

}
