using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_PowerUp : MonoBehaviour
{
    [SerializeField] private float HealthValue;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player_Health>().AddHealth(HealthValue);
            gameObject.SetActive(false);
        }
    }
}
