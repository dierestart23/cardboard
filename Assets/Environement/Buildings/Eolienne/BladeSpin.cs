using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpin : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("The axis to rotate around. (e.g., (1, 0, 0) for X-axis)")]
    public Vector3 rotationAxis = Vector3.up; // Default is Y-axis

    [Tooltip("Rotation speed in degrees per second.")]
    public float rotationSpeed = 90f;

    private void Update()
    {
        // Rotate the object around the specified axis at the defined speed
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);
    }
}