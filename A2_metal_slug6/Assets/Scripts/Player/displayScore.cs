using System.Collections;
using TMPro;
using UnityEngine;

public class displayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int playerScore;  // Keeping this private

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;  // Initialize the score
        UpdateScoreText();
    }

    // Update the score text only when necessary
    public void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + playerScore;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            playerScore += 10;  // Increment score when a coin is collected
            UpdateScoreText();  // Update the score display
            Destroy(collision.gameObject);  // Destroy the collected coin
        }
    }
}
