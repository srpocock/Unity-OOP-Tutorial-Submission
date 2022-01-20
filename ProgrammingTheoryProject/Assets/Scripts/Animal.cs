using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    public float moveSpeed = 5.0f;
    public float jumpStrength = 0.1f;

    private Vector3 direction;

    public Rigidbody animalRb;
    private bool isOnGround = true;

    void Start()
    {
        animalRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void GetInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical).normalized;
    }

    void Jump()
    {
        animalRb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        isOnGround = false;
    }

    void Movement()
    {
        // 3d traversing movement
        GetInput();

        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        // Jump Script
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            Jump();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

}
