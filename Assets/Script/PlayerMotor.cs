using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] public MoveSettings _settings = null;
    [SerializeField] [Range(0.0f, 69.0f)] protected float walkSpeed = 21.0f;
    [SerializeField] [Range(0.0f, 69.0f)] protected float sprintSpeed = 30.0f;
    [SerializeField] [Range(0.0f, 69.0f)] protected float crouchedSpeed = 0.61f;
    [SerializeField] [Range(-69.0f, 69.0f)] protected float playerGravity = -9.81f;
    [SerializeField] [Range(0.0f, 69.0f)] protected float jumpForce = 3f;
    [SerializeField] protected float playerCrouchTimer = 0f;
    [SerializeField] protected bool lockCursor = true;    

    protected float speed;
    protected float jump;
    protected float gravity;
    protected float crouchTimer;

    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    private bool isGrounded;
    private bool lerpCrouch;
    private bool isCrouching;
    private bool isSprinting;

    // Awake is called before Start
    void Awake()
    {
        //Lock cursor on game
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        /***** Set Default Values *****/

        //Set default speed
        if (walkSpeed != _settings.speed)
            speed = walkSpeed;
        else
            speed = _settings.speed;

        //Set default gravity
        if (gravity != _settings.gravity)
            gravity = playerGravity;
        else
            gravity = _settings.gravity;

        //Set default jumpForce
        if (walkSpeed != _settings.jumpForce)
            jump = jumpForce;
        else
            jump = _settings.jumpForce;

        //Set default crouchTimer
        if (crouchTimer != _settings.crouchTimer)
            crouchTimer = playerCrouchTimer;
        else
            crouchTimer = _settings.crouchTimer;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.fixedDeltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (isCrouching) {
                controller.height = Mathf.Lerp(controller.height, 1, p);
                speed = crouchedSpeed;
            } else {
                controller.height = Mathf.Lerp(controller.height, 2, p);
                speed = walkSpeed;
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    //receive the inputs for our InputManger.cs and apply them to our character controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);

        //Debug.Log("Player Velocity (Y): " + playerVelocity.y);
    }

    public void Jump() 
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jump * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        /*if (isSprinting)
        {
            //Perform a slide
        }*/

        isCrouching = !isCrouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        if (isGrounded && !isCrouching)
        {
            isSprinting = !isSprinting;
            if (isSprinting)
                speed = sprintSpeed;
            else
                speed = walkSpeed;
        }
    }
}
