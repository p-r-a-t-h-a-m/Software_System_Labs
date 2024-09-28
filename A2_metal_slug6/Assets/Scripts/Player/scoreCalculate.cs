using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculate : MonoBehaviour
{
    public displayScore displayScore; 

    private void Start()
    {
        displayScore = GameObject.FindGameObjectWithTag("Player").GetComponent<displayScore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Increment player score by 50 when colliding with an enemy
            displayScore.playerScore += 50;

            // Force the score to be updated
            displayScore.UpdateScoreText();
        }
    }
}
