using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    private float moveSpeed = 5f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float lookSensitivity = 2f;
    public float jumpForce = 5f;
    public float maxVelocityChange = 10f;
    public float minLookAngle = -60f; // Minimum vertical angle
    public float maxLookAngle = 60f;

    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;
    public float staminaThreshold = 10f; // Threshold to allow running again

    public float walkFOV = 60f;
    public float runFOV = 75f;
    public float fovTransitionSpeed = 2f;

    private Rigidbody rb;
    private Camera playerCamera;
    private float currentCameraRotationX = 0f;
    private bool isGrounded;
    private bool canRun = true; // Determines if the player can run

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            Debug.LogError("No Camera found. Please make sure the Main Camera is a child of the player object.");
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentStamina = maxStamina;

    }

    void Update()
    {
        RotatePlayer();
        CheckGroundStatus();
        HandleStamina();
        HandleCameraFOV();
    }

    void FixedUpdate()
    {
        MovePlayer();
        Jump();
    }

    void RotatePlayer()
    {
        float yRotation = Input.GetAxis("Mouse X") * lookSensitivity;
        transform.Rotate(0, yRotation, 0);

        float xRotation = -Input.GetAxis("Mouse Y") * lookSensitivity;
        currentCameraRotationX += xRotation;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, minLookAngle, maxLookAngle);

        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 targetVelocity = new Vector3(moveHorizontal, 0, moveVertical);
        targetVelocity = transform.TransformDirection(targetVelocity);

        if (Input.GetKey(KeyCode.LeftShift) && canRun && currentStamina > 0 && targetVelocity.sqrMagnitude > 0)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, Time.deltaTime * 10);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, Time.deltaTime * 10);
        }

        targetVelocity *= moveSpeed;

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void Jump()
    {
        if (isGrounded && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, CalculateJumpVerticalSpeed(), rb.velocity.z);
        }
    }

    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * jumpForce * Mathf.Abs(Physics.gravity.y));
    }

    void CheckGroundStatus()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void HandleStamina()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 targetVelocity = new Vector3(moveHorizontal, 0, moveVertical);
        targetVelocity = transform.TransformDirection(targetVelocity);

        if (Input.GetKey(KeyCode.LeftShift) && canRun && currentStamina > 0 && targetVelocity.sqrMagnitude > 0)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina < 0)
            {
                currentStamina = 0;
                canRun = false; // Prevent running when stamina is zero
            }
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }

            if (currentStamina >= staminaThreshold)
            {
                canRun = true; // Allow running again when stamina reaches threshold
            }
        }

        // Update the stamina UI
    }

    void HandleCameraFOV()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 targetVelocity = new Vector3(moveHorizontal, 0, moveVertical);
        targetVelocity = transform.TransformDirection(targetVelocity);

        if (Input.GetKey(KeyCode.LeftShift) && canRun && currentStamina > 0 && targetVelocity.sqrMagnitude > 0)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, runFOV, Time.deltaTime * fovTransitionSpeed);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, walkFOV, Time.deltaTime * fovTransitionSpeed);
        }
    }
}
