using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    [Header("Vehicle Settings")]
    public float accelerationForce = 1500f; // Forward acceleration force
    public float turnTorque = 50f;         // Torque for turning
    public float brakeForce = 3000f;      // Braking force
    public float maxSpeed = 20f;          // Maximum speed of the vehicle

    [Header("Wheel Settings")]
    public Transform frontWheel;
    public Transform backWheel;
    public float wheelRotationSpeed = 10f; // Speed for visual wheel rotation

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get player inputs
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // Rotate wheels visually
        RotateWheels();
    }

    void FixedUpdate()
    {
        // Apply forward or backward force
        if (moveInput != 0)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * moveInput * accelerationForce * Time.fixedDeltaTime);
            }
        }

        // Apply turning torque
        if (turnInput != 0)
        {
            rb.AddTorque(transform.up * turnInput * turnTorque * Time.fixedDeltaTime);
        }

        // Apply brake force if no acceleration input
        if (moveInput == 0 && rb.velocity.magnitude > 0.1f)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.fixedDeltaTime);
        }
    }

    private void RotateWheels()
    {
        if (frontWheel != null && backWheel != null)
        {
            float rotation = moveInput * wheelRotationSpeed * Time.deltaTime;
            frontWheel.Rotate(Vector3.forward * rotation);
            backWheel.Rotate(Vector3.forward * rotation);
        }
    }
}
