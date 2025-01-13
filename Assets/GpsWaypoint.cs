using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GpsWaypoint : MonoBehaviour
{
    public Image GpsImage;
    public Transform target;

    private void Update()
    {
        // Update the position on x and y axes only
        Vector3 newPosition = new Vector3(target.position.x, GpsImage.transform.position.y, target.position.z);
        GpsImage.transform.position = newPosition;
    }
}
