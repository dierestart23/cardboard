using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSPlayerFollow : MonoBehaviour
{
    public Image playerGpsImage;
    public Transform player;

    private void Update()
    {
        // Update the position on x and y axes only
        Vector3 newPosition = new Vector3(player.position.x, playerGpsImage.transform.position.y, player.position.z);
        playerGpsImage.transform.position = newPosition;

        // Update the rotation on the z axis only
        Quaternion newRotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
        playerGpsImage.transform.localRotation = newRotation;
    }
}
