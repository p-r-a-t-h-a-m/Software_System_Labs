using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    
    [SerializeField] private float speed;
    private Rigidbody2D body;
    // Start is called before the first frame update
    private void Awake()
    {
        body=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput=Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput*speed, body.velocity.y);
        
        // Flip the character when moving left/right

        if(horizontalInput>0.01f)
        {
            transform.localScale= new Vector3(3,3,3);
        }
        else if(horizontalInput<0.01f)
        {
            transform.localScale= new Vector3(-3,3,3);
        }
        if( Input.GetKey(KeyCode.Space))
        {
            body.velocity=new Vector2(body.velocity.x, speed);
        }
    }
}
