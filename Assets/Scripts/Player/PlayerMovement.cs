using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;
    
    [SerializeField] private LayerMask Ground;      //"Terrain" Layer is used to detect Ground which is passed as serial input
    [SerializeField] private LayerMask Trap;

    private float jumpHeight = 30f;
    private float Xval;
    private float speed = 10f;
    private float posY;
    private bool isRigid = true;
    private bool facingLeft = false;
    private bool isCrouched = false;
    private float fireSourceOffset = 0.25f;

    private enum Playerstate { idle, run, jump, crouch };


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Xval = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(Xval * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && ( Grounded() || onTrap() ) )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            if (isCrouched)
            {
                crouchHandle(false);
            }
            isCrouched = false;
        }

        if (!isCrouched && downKey() && Grounded())
        {
            crouchHandle(true);
            isCrouched = true;
        }

        if (isCrouched && (Xval > 0f || upKey()))
        {
            isCrouched = false;
            crouchHandle(false);
        }

        if (isCrouched)
        { 
            passThroughPlatform();
        }

        ChangeAnimations();

    }
    private void ChangeAnimations()
    {
        Playerstate state;

        if (Xval < -0.001f)
        {
            state = Playerstate.run;
            if (facingLeft == false)
            {
                facingLeft = true;
                anim.SetBool("facingLeft", true);
                flipDir();
            }
        }
        else if (Xval > 0.001f)
        {
            state = Playerstate.run;
            if (facingLeft == true)
            {
                facingLeft = false;
                anim.SetBool("facingLeft", false);
                flipDir();
            }
        }
        else
        {
            if (isCrouched)
                state = Playerstate.crouch;
            else
                state = Playerstate.idle;
        }



        if (!isCrouched && (rb.velocity.y > 0.001f || rb.velocity.y < -0.001f))
        {
            state = Playerstate.jump;
        }

        anim.SetInteger("PlayerState", (int)state);
    }

    private void passThroughPlatform()
    {

        if (Input.GetKeyDown("s") && Grounded())
        {
            posY = transform.position.y;
            isRigid = false;
            bc.enabled = false;
        }

        if (!isRigid && (posY - transform.position.y >= 2f))
        {
            isRigid = true;
            bc.enabled = true;

            //un-crouch the player when passed through platform.
            crouchHandle(false);
            isCrouched = false;
            anim.SetInteger("PlayerState", (int)Playerstate.idle);
        }
    }

    private void flipDir()
    {
        transform.Rotate(0f, 180f, 0f);

    }

    private void crouchHandle(bool crouch)
    {
        float xSize = bc.size.x;
        float ySize = bc.size.y;
        GameObject fireSource = transform.GetChild(0).gameObject;
     
        //resize the box collider according to the sprite of crouch and standing position
        bc.size = new Vector2(ySize, xSize);

        //adjust the fire source point according to standing/crouched position
        if (crouch)
        {
            fireSource.transform.position = new Vector2(fireSource.transform.position.x, fireSource.transform.position.y - fireSourceOffset);
        }
        else
        {
            fireSource.transform.position = new Vector2(fireSource.transform.position.x, fireSource.transform.position.y + fireSourceOffset);
        }
    }
    private bool downKey()
    {
        return Input.GetKeyDown("s") || Input.GetKeyDown("down");
    }
    private bool upKey()
    {
        return Input.GetKeyDown("w") || Input.GetKeyDown("up");
    }

    //Grounded() is used to detect that the player is grounded or not
    private bool Grounded()
    {
        // BoxCast create a box around the box collider of the player
        // arguments:-
        // origin of boxcast = origin of player's box collider
        // size of boxcast = size of player's box collider
        // rotation of box = 0 (NO rotation)
        // Vector2.down & 0.1f: shifting boxcast little bit below to the player boxcollider, so that it can overlap to the ground and detect it, also it wil not detect it when player collide the ground from sides.

        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, Ground);
    }
    private bool onTrap()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, Trap);
    }

}
