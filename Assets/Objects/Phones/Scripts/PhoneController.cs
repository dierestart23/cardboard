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

    public bool soundOn;

    public float smooth;
    public float swayMultiplier;

    public float timeOfDay;

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
        UpdateTimeDisplay();
        PhoneSway();

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
            characterController.canMove = !phoneActive;
            phoneAnimator.SetBool("PhoneActive", phoneActive);
        }

        // Manage cursor visibility
        if (phoneActive)
        {
            if(Input.GetMouseButton(2))
            {
                characterController.canMove = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                
            } else {
                characterController.canMove = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            

            // Update time on the phone when it's active
            
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
            timeOfDay = dayNightCycle.timeOfDay;
            float minutes = (timeOfDay - Mathf.Floor(timeOfDay)) * 60;

            // Format the time into "HH:MM" format
            int hours = Mathf.FloorToInt(timeOfDay);
            int mins = Mathf.FloorToInt(minutes);
            string timeString = string.Format("{0:D2}:{1:D2}", hours, mins);

            // Set the time to the TMP Text
            timeDisplay.text = timeString;
        }
    }

    private void PhoneSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
        if(Input.GetMouseButton(2))
        {
            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
            Quaternion targetRotation = rotationX * rotationY;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime); 
        } else
        {
            Quaternion rotationX = Quaternion.AngleAxis(0, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(0, Vector3.up);
            Quaternion targetRotation = rotationX * rotationY;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime); 
        }
    }
}
