using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player;  // Reference to the player's transform
    [SerializeField] private float followSpeed = 0.2f;  // Speed of the camera following the player
    [SerializeField] private Vector2 followOffset;  // Offset for smoother camera movement
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // Set the target position to follow both x and y axes
        targetPosition = new Vector3(player.position.x + followOffset.x, player.position.y + followOffset.y, transform.position.z);

        // Smoothly move the camera to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);
    }
}
