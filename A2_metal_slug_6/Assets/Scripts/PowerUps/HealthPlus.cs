using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlus : MonoBehaviour
{
    [SerializeField] Player_Health playerHealth;
    [SerializeField] private int HealthValue;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            playerHealth.AddHealth(HealthValue);
            gameObject.SetActive(false);
        }
    }
}
