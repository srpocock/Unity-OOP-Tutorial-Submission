using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    public bool isSelected = false;

    protected Rigidbody animalRb;
    protected Animator animalAnim;

    // Rotational
    private float _turnSmoothTime = 0.2f;
    public float turnSmoothTime
    {
        protected set
        {
            if (value >= 0f)
            {
                _turnSmoothTime = value;
            }
            else
            {
                Debug.LogError("Turning time must be non-negative");
            }
        }
        get
        {
            return _turnSmoothTime;
        }
    }

    private float turnSmoothVelocity;
    protected float angle;
    private Vector3 inputDirection;

    // Walking forces
    private float _maxMoveSpeed = 6.0f;
    public float maxMoveSpeed
    {
        protected set
        {
            if (value >= 0f)
            {
                _maxMoveSpeed = value;
            }
            else
            {
                Debug.LogError("Max move speed must be non-negative");
            }
        }
        get
        {
            return _maxMoveSpeed;
        }
    }

    // Walking animation
    private float _walkAnimDivider;
    public float walkAnimDivider
    {
        protected set
        {
            if (value >= 0f)
            {
                _walkAnimDivider = value;
            }
            else
            {
                Debug.LogError("Max walk animation speed must be non-negative");
            }
        }
        get
        {
            return _walkAnimDivider;
        }
    }

    private float _walkAccel = 30.0f;
    public float walkAccel
    {
        protected set
        {
            if (value >= 0f)
            {
                _walkAccel = value;
            }
            else
            {
                Debug.LogError("Walk acceleration must be non-negative");
            }
        }
        get
        {
            return _walkAccel;
        }
    }

    // Jump
    private float _jumpStrength = 5.0f;
    public float jumpStrength
    {
        protected set
        {
            if (value >= 0f)
            {
                _jumpStrength = value;
            }
            else
            {
                Debug.LogError("Jump strength must be non-negative");
            }
        }
        get
        {
            return _jumpStrength;
        }
    }
    public bool isOnGround { protected set; get; }

    protected virtual void Awake()
    {
        walkAnimDivider = maxMoveSpeed;
        isOnGround = true;
        animalRb = GetComponent<Rigidbody>();
        animalAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        HandleWalk();
        HandleJump();
    }

    void GetWalkInput()
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
        if (isSelected && Input.GetKey(KeyCode.Space) && isOnGround)
        {
            Jump();
        }
    }

    protected virtual void HandleWalk()
    {
        // 3d traversing movement

        float groundSpeed = new Vector2(animalRb.velocity.x, animalRb.velocity.z).magnitude;
        animalAnim.SetFloat("Speed_f", groundSpeed / walkAnimDivider);

        if (!isSelected) {
            return;
        }

        GetWalkInput();

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            animalRb.rotation = Quaternion.Euler(0, angle, 0);

            if (groundSpeed < maxMoveSpeed)
            {
                animalRb.AddForce(inputDirection * walkAccel);
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

}
