using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSCamFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The object to follow

    [Header("Movement Settings")]
    public float smoothSpeed = 5f; // Speed of the smoothing
    public float yOffset = 2f; // Adjustable Y offset

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to SmoothFollow script.");
            return;
        }

        // Get the target position with the adjustable Y offset
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yOffset, target.position.z);

        // Smoothly interpolate the current position towards the target position (only X and Z axes)
        Vector3 smoothedPosition = Vector3.Lerp(
            new Vector3(transform.position.x, targetPosition.y, transform.position.z),
            new Vector3(targetPosition.x, targetPosition.y, targetPosition.z),
            smoothSpeed * Time.deltaTime
        );

        // Update the position of this object
        transform.position = smoothedPosition;
    }
}
