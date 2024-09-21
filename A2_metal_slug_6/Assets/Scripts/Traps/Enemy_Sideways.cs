using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movementDistance;
    [SerializeField] private float movementSpeed;
    private bool left;
    private float leftEdge;
    private float rightEdge;
    private void Awake()
    {
        leftEdge=transform.position.x-movementDistance;
        rightEdge=transform.position.x+movementDistance;
    }

    private void Update()
    {
        if(left)
        {
            if(transform.position.x>leftEdge)
            {
                transform.position= new Vector3(transform.position.x-movementSpeed*Time.deltaTime,transform.position.y,transform.position.z);
            }
            else
            {
                left=false;
            }
        }
        else
        {
            if(transform.position.x<rightEdge)
            {
                transform.position= new Vector3(transform.position.x+movementSpeed*Time.deltaTime,transform.position.y,transform.position.z);
            }
            else
            {
                left=true;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag=="Player")
        {
            collision.GetComponent<Player_Health>().TakeDamage(damage);

        }
    }
}
