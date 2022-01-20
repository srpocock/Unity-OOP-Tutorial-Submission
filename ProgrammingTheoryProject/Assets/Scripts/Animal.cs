using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;
    protected float angle;

    protected float maxMoveSpeed = 6.0f;

    private float _moveSpeed = 30.0f;
    public float moveSpeed { 
    set
    {
        _moveSpeed = value;
    }
    get
    {
        return _moveSpeed;
    }
    
    }

    protected float jumpStrength = 5.0f;

    private Vector3 inputDirection;

    public Rigidbody animalRb;
    protected bool isOnGround = true;

    public virtual void Awake()
    {
        animalRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
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

        float groundSpeed = new Vector2(animalRb.velocity.x, animalRb.velocity.z).magnitude;

        if (inputDirection.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            animalRb.rotation = Quaternion.Euler(0, angle, 0);
        
            if (groundSpeed < maxMoveSpeed)
            {
                animalRb.AddForce(inputDirection * moveSpeed);
            }
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
