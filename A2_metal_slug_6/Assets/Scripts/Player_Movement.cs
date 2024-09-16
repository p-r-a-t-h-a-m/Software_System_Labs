using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask walllayer;

    private float wallJumpCooldown;
    private float horizontalInput;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    private void Awake()
    {
        body=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        boxCollider=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput=Input.GetAxis("Horizontal");
        //body.velocity = new Vector2(horizontalInput*speed, body.velocity.y);
        
        // Flip the character when moving left/right

        if(horizontalInput>0.01f)
        {
            transform.localScale= new Vector3(3,3,3);
        }
        else if(horizontalInput<-0.01f)
        {
            transform.localScale= new Vector3(-3,3,3);
        }

        anim.SetBool("run",horizontalInput!=0);
        anim.SetBool("grounded",isGrounded());

        //Delays between wall jumps

        if(wallJumpCooldown>0.2f)
        {
            body.velocity = new Vector2(horizontalInput*speed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale=0;
                body.velocity=Vector2.zero;
            }
            else
            {
                body.gravityScale=7;
            }
            if(Input.GetKey(KeyCode.Space))
            {
            jump();   
            }
        }
        else
        {
            wallJumpCooldown+=Time.deltaTime;
        }
    }
    private void jump()
    {
        if(isGrounded())
        {
        body.velocity=new Vector2(body.velocity.x, jumpSpeed);
        anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput==0)
            {
                body.velocity=new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
                transform.localScale=new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            }
            else
                body.velocity=new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);
            wallJumpCooldown=0;
            
        }
    }

    private bool isGrounded() 
    {
        RaycastHit2D raycast= Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundlayer);
        return raycast.collider!=null;
    }

    private bool onWall()
    {
        RaycastHit2D raycast= Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,walllayer);
        return raycast.collider!=null;
    }

    public bool canAttack()
    {
        return(horizontalInput==0 && isGrounded() && !onWall());
    }
}
