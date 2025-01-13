using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppOpener : MonoBehaviour
{
    public Transform objectToMove; // Assign the object to move and resize
    public Vector3 targetLocalPosition; // Target local position
    public Vector3 targetLocalScale; // Target local scale
    public float transitionSpeed = 2f; // Speed of the transition
    public bool activateTransition = false; // Trigger for the transition

    public Vector3 initialLocalPosition;
    public Vector3 initialLocalScale;
    private float lerpProgress = 0f;
    public HomeButton homeButton;

    void Update()
    {
        if (objectToMove == null) return;

        // Adjust lerpProgress based on the activation state
        if (activateTransition)
        {
            objectToMove.gameObject.SetActive(true);
            lerpProgress += Time.deltaTime * transitionSpeed;
        }
        else
        {
            lerpProgress -= Time.deltaTime * transitionSpeed;
        }

        // Clamp lerpProgress between 0 and 1
        lerpProgress = Mathf.Clamp01(lerpProgress);

        // Interpolate local position and scale based on lerpProgress
        objectToMove.localPosition = Vector3.Lerp(initialLocalPosition, targetLocalPosition, lerpProgress);
        objectToMove.localScale = Vector3.Lerp(initialLocalScale, targetLocalScale, lerpProgress);
    }

    public void OpenApp()
    {
        activateTransition = true;
        homeButton.openedApp = this;
    }
}