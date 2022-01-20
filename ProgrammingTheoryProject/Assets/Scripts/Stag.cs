using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stag : Animal
{

    private float chargeSpeed = 2.0f;
    private bool hasCharged = false;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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
        hasCharged = true;

        float timePassed = 0.0f;
        while (timePassed < 1.0f)
        {
            timePassed += Time.deltaTime;
            transform.Translate(chargeDirection * Time.deltaTime * chargeSpeed, Space.World);
        }

        StartCoroutine(WaitToCharge());
        
    }

    IEnumerator WaitToCharge()
    {
        yield return new WaitForSeconds(1);
        hasCharged = false;
    }

}
