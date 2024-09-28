using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Cached components
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;

    // Serialized fields for ground and trap layers
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Trap;

    // Player movement variables
    private float jumpHeight = 30f;
    private float speed = 10f;
    private bool isRigid = true;
    private bool facingLeft = false;
    private bool isCrouched = false;
    private float fireSourceOffset = 0.25f;

    private float Xval;
    private float posY;

    private enum PlayerState { Idle, Run, Jump, Crouch }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Xval = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Xval * speed, rb.velocity.y);

        // Jump logic
        if (Input.GetButtonDown("Jump") && (Grounded() || OnTrap()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            if (isCrouched)
                CrouchHandle(false);
            isCrouched = false;
        }

        // Crouch logic
        if (!isCrouched && DownKey() && Grounded())
        {
            CrouchHandle(true);
            isCrouched = true;
        }

        if (isCrouched && (Xval > 0f || UpKey()))
        {
            isCrouched = false;
            CrouchHandle(false);
        }

        if (isCrouched)
            PassThroughPlatform();

        ChangeAnimations();
    }

    private void ChangeAnimations()
    {
        PlayerState state;

        // Handle running state
        if (Xval < -0.001f)
        {
            state = PlayerState.Run;
            if (!facingLeft)
            {
                facingLeft = true;
                FlipDirection();
            }
        }
        else if (Xval > 0.001f)
        {
            state = PlayerState.Run;
            if (facingLeft)
            {
                facingLeft = false;
                FlipDirection();
            }
        }
        else
        {
            state = isCrouched ? PlayerState.Crouch : PlayerState.Idle;
        }

        // Handle jumping state
        if (!isCrouched && (rb.velocity.y > 0.001f || rb.velocity.y < -0.001f))
            state = PlayerState.Jump;

        anim.SetInteger("PlayerState", (int)state);
    }

    private void PassThroughPlatform()
    {
        if (DownKey() && Grounded())
        {
            posY = transform.position.y;
            isRigid = false;
            bc.enabled = false;
        }

        if (!isRigid && (posY - transform.position.y >= 2f))
        {
            isRigid = true;
            bc.enabled = true;

            // Reset crouch state
            CrouchHandle(false);
            isCrouched = false;
            anim.SetInteger("PlayerState", (int)PlayerState.Idle);
        }
    }

    private void FlipDirection()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void CrouchHandle(bool crouch)
    {
        GameObject fireSource = transform.GetChild(0).gameObject;
        float xSize = bc.size.x;
        float ySize = bc.size.y;

        // Adjust collider size for crouching
        bc.size = new Vector2(ySize, xSize);

        // Adjust the fire source point position
        if (crouch)
        {
            fireSource.transform.position = new Vector2(fireSource.transform.position.x, fireSource.transform.position.y - fireSourceOffset);
        }
        else
        {
            fireSource.transform.position = new Vector2(fireSource.transform.position.x, fireSource.transform.position.y + fireSourceOffset);
        }
    }

    private bool DownKey()
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    private bool UpKey()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }

    // Ground detection
    private bool Grounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, Ground);
    }

    // Trap detection
    private bool OnTrap()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, Trap);
    }
}
