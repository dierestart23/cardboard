using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab of the ball to shoot
    public float shootForce = 500f; // Force to apply to the ball
    public Transform shootPoint; // Point from which the balls are shot (e.g., Camera center)
    
    void Update()
    {
        // Check for left mouse button click or touch input
        if (Input.GetMouseButtonDown(0))
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
        // Ensure ball prefab and shoot point are assigned
        if (ballPrefab == null || shootPoint == null)
        {
            Debug.LogWarning("Ball prefab or shoot point is not assigned!");
            return;
        }

        // Instantiate the ball at the shoot point
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);

        // Ensure the ball has a Rigidbody component
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("The ball prefab must have a Rigidbody component!");
            Destroy(ball);
            return;
        }

        // Apply force to the ball in the direction of the camera's forward vector
        rb.AddForce(shootPoint.forward * shootForce);
    }
}