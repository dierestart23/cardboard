using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEnter : MonoBehaviour
{
    public float enterRange = 5f;
    public GameObject player; // Reference to the player GameObject
    private Transform originalParent;
    private BicycleVehicle currentVehicle; // Reference to the current vehicle

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        // Enter vehicle logic
        if (Input.GetKeyDown(KeyCode.E)) // Change E to the desired key
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enterRange))
            {
                // Check if the hit object has a Rigidbody and is a child of another object
                Rigidbody hitRigidbody = hit.transform.GetComponent<Rigidbody>();
                if (hitRigidbody != null && hit.transform.parent != null)
                {
                    if (hit.transform.gameObject.tag == "VehicleDoor")
                    {
                        BicycleVehicle vehicleScript = hit.transform.GetComponentInParent<BicycleVehicle>();

                        if (vehicleScript != null)
                        {
                            currentVehicle = vehicleScript; // Assign current vehicle
                            vehicleScript.isOn = true; // Activate the isOn boolean
                            Debug.Log("Vehicle is now On");

                            // Find the camPos transform
                            Transform camPos = hit.transform.GetComponentInParent<Transform>().Find("CamPos");

                            if (camPos != null)
                            {
                                originalParent = transform.parent; // Save original parent
                                transform.SetParent(camPos);
                                transform.localPosition = Vector3.zero; // Reset position relative to camPos
                                transform.localRotation = Quaternion.identity; // Reset rotation

                                if (player != null)
                                {
                                    player.SetActive(false); // Deactivate player
                                    player.transform.SetParent(camPos); // Set as child of camPos
                                    player.transform.localPosition = Vector3.zero; // Reset position relative to camPos
                                    player.transform.localRotation = Quaternion.identity; // Reset rotation
                                    Debug.Log("Player is now attached to camPos and deactivated.");
                                }
                                else
                                {
                                    Debug.LogWarning("Player GameObject is not assigned!");
                                }
                            }
                            else
                            {
                                Debug.LogWarning("camPos transform not found in the vehicle hierarchy!");
                            }
                        }
                        else
                        {
                            Debug.LogWarning("No BicycleVehicle script found on the parent!");
                        }
                    }
                }
            }
        }

        // Exit vehicle logic
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Press Shift to exit vehicle
        {
            if (player != null && !player.activeSelf) // Check if player is deactivated and currently in the vehicle
            {
                // Detach player from vehicle
                player.transform.SetParent(null); // Make player parentless
                player.SetActive(true); // Reactivate the player
                //player.transform.localPosition = Vector3.zero; // Reset position to origin
                player.transform.rotation = Quaternion.identity; // Reset rotation

                // Set transform to CamPos inside the player
                Transform playerCamPos = player.transform.Find("CamPos");
                if (playerCamPos != null)
                {
                    transform.SetParent(playerCamPos);
                    transform.localPosition = Vector3.zero; // Reset position relative to CamPos
                    transform.localRotation = Quaternion.identity; // Reset rotation
                    Debug.Log("Transform is now reparented to player's CamPos.");
                }
                else
                {
                    Debug.LogWarning("CamPos not found inside the player!");
                }

                // Deactivate the vehicle's "isOn" boolean
                if (currentVehicle != null)
                {
                    currentVehicle.isOn = false;
                    Debug.Log("Vehicle is now Off.");
                    currentVehicle = null; // Clear the reference
                }
            }
        }
    }
}
