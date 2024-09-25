using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrap : Enemy_Damage
{
    [Header ("FollowTrap")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    private Vector3 target;
    private bool attacking;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTime;
    private Vector3 [] directions = new Vector3 [4]; 

    private void OnEnable() 
    {
        Stop();    
    }
    private void Update()
    {
        if(attacking)
            transform.Translate(target*Time.deltaTime*speed);
        else
        {
            checkTime+=Time.deltaTime;
            if(checkTime > checkDelay)
                checkForPlayer();
        }
    }
    private void checkForPlayer()
    {
        calculateDirections();
        for(int i=0; i<directions.Length;i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit=Physics2D.Raycast(transform.position, directions[i],range, playerLayer);

            if(hit.collider != null && !attacking)
            {
                attacking=true;
                target=directions[i];
                checkTime=0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        base.OnTriggerEnter2D(collision);
        Stop();    
    }

    private void Stop()
    {
        target=transform.position;
        attacking=false;

    }
    private void calculateDirections()
    {
        directions[0]=transform.right *range;
        directions[1]=-transform.right *range;
        directions[2]=transform.up*range;
        directions[3]=-transform.up *range;
    }
}
