using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleRotation : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject player;
    private Animator anim;
    internal SpriteRenderer sr;

    [SerializeField] private Sprite[] guns;
    internal int currentGun = 0;  //basic gun at 0 index

    private Vector3 mousePos;
    Vector3 rotate;
    private bool rotateRifleLeft = false;
    private float rotZ;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
        anim = player.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        changeGun();
        followMouse();
    }

    private void changeGun()
    {
        sr.sprite = guns[currentGun];
    }

    private void followMouse()
    {
        // Get the mouse position in world space
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction to point the rifle
        rotate = mousePos - transform.position;
        rotZ = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;

        // Flip rifle and correct rotation when player is facing left or right
        if (anim.GetBool("facingLeft"))
        {
            sr.flipY = true;  // Flip the rifle on the Y axis when facing left
            rotZ = Mathf.Clamp(rotZ, -90f, 90f); // Constrain rotation to 90 degrees
        }
        else
        {
            sr.flipY = false;
        }

        // Set the rotation of the rifle based on the mouse position and player facing direction
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
