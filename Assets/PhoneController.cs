using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{
    [Header("Reference")]
    public DayNightCycle dayNightCycle;
    public Animator phoneAnimator;
    public CharacterController characterController;
    public PickUpScript pickUp;
    public TMP_Text timeDisplay;

    public bool phoneActive;
    private bool canBeUsed;

     // TMP Text to display the time

     // Reference to the DayNightCycle script

    // Start is called before the first frame update
    void Start()
    {
        if (timeDisplay == null)
        {
            Debug.LogWarning("Time Display TMP_Text is not assigned in the PhoneController script.");
        }
        if (dayNightCycle == null)
        {
            Debug.LogWarning("DayNightCycle script is not assigned in the PhoneController script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle phone visibility
        if (Input.GetKeyDown(KeyCode.I) && canBeUsed == true)
        {
            if (pickUp.heldObj != null)
            {
                pickUp.canDrop = true;
                pickUp.DropObject();
            }

            canBeUsed = false;
            phoneActive = !phoneActive;
            characterController.canMove = !characterController.canMove;
            phoneAnimator.SetBool("PhoneActive", phoneActive);
        }

        // Manage cursor visibility
        if (phoneActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Update time on the phone when it's active
            UpdateTimeDisplay();
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void CanBeUsed()
    {
        canBeUsed = true;
    }

    private void UpdateTimeDisplay()
    {
        if (timeDisplay != null && dayNightCycle != null)
        {
            // Get the time from DayNightCycle
            float timeOfDay = dayNightCycle.timeOfDay;
            float minutes = (timeOfDay - Mathf.Floor(timeOfDay)) * 60;

            // Format the time into "HH:MM" format
            int hours = Mathf.FloorToInt(timeOfDay);
            int mins = Mathf.FloorToInt(minutes);
            string timeString = string.Format("{0:D2}:{1:D2}", hours, mins);

            // Set the time to the TMP Text
            timeDisplay.text = timeString;
        }
    }
}
