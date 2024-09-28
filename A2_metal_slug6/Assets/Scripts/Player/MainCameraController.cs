using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;  // Adjust this for smoothness
    [SerializeField] private Vector2 minBounds;  // Minimum x and y limits
    [SerializeField] private Vector2 maxBounds;  // Maximum x and y limits

    private void Update()
    {
        // Target camera position following the player (only in x direction)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // Smoothly interpolate to the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Clamp the camera's x position within specified bounds
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);

        // Apply the smoothed and clamped position to the camera
        transform.position = smoothedPosition;
    }
}
