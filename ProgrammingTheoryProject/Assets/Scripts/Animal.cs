using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;
    protected float angle;

    public float moveSpeed = 5.0f;
    protected float jumpStrength = 5.0f;

    private Vector3 inputDirection;

    public Rigidbody animalRb;
    protected bool isOnGround = true;

    public virtual void Start()
    {
        animalRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        HandleWalk();
        HandleJump();
    }

    void GetInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0, vertical).normalized;
    }

    void Jump()
    {
        animalRb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        isOnGround = false;
    }

    void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            Jump();
        }
    }

    void HandleWalk()
    {
        // 3d traversing movement
        GetInput();

        if (inputDirection.magnitude >= 0.1f)
        {
            transform.Translate(inputDirection * Time.deltaTime * moveSpeed, Space.World);

            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

}
