using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    public float turnSpeed = 5.0f;
    public float moveSpeed = 5.0f;
    public float jumpStrength = 0.2f;

    public Vector2 directionVector;

    public Rigidbody animalRb;
    private bool isOnGround = true;

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            Jump();
        }
    }

    void GetInput()
    {
        directionVector.x = Input.GetAxis("Horizontal");
        directionVector.y = Input.GetAxis("Vertical");

        //directionVector = directionVector.normalized;
    }

    void Jump()
    {
        animalRb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        isOnGround = false;
    }

    void Movement()
    {
        GetInput();
        transform.Translate(directionVector.x * Time.deltaTime * moveSpeed, 0, directionVector.y * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

}
