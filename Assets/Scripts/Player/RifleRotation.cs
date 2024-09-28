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
    float rotX;
    float rotZ;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        Debug.Log("currentGun= "+currentGun);
        sr.sprite = guns[currentGun];
    }
    private void followMouse()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        rotate = mousePos - transform.position;
        rotZ = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;

        rotX = transform.rotation.x;
        if (anim.GetBool("facingLeft") && !rotateRifleLeft)
        {
            rotateRifleLeft = true;
            rotX = 180f;
        }
        if (!anim.GetBool("facingLeft") && rotateRifleLeft)
        {
            rotateRifleLeft = false;
            rotX = 0;
        }

        transform.rotation = Quaternion.Euler(rotX, transform.rotation.y, transform.rotation.z);

        if (Mathf.Abs(rotZ) < 90f && !rotateRifleLeft)
        {
            sr.flipY = false;
            transform.rotation = Quaternion.Euler(rotX, transform.rotation.y, rotZ);
        }
        if (Mathf.Abs(rotZ) > 90f && rotateRifleLeft)
        {
            sr.flipY = true;
            transform.rotation = Quaternion.Euler(rotX, transform.rotation.y, rotZ);
        }
        

    }
}
