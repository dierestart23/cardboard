using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSPointer : MonoBehaviour
{
    public Transform player;
    public Transform target;

    private float rotationSpeed = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = newPosition;
        if (target != null)
        {
            // Determine the direction to the target
            Vector3 direction = target.position - transform.position;

            // Calculate the rotation needed to face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
