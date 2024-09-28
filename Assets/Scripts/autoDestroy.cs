using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance >50f)
        {
            Destroy(gameObject);
        }
    }
}
