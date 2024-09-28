using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class displayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    internal int playerScore;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + playerScore;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            playerScore += 10;
            Destroy(collision.gameObject);
        }
    }

}
