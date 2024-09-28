using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerLife playerLife;  // Cache reference to PlayerLife component
    [SerializeField] private Image totalLifeBar;
    [SerializeField] private Image currentLifeBar;

    private void Start()
    {
        // Cache the PlayerLife component
        playerLife = player.GetComponent<PlayerLife>();
        totalLifeBar.fillAmount = 1f;  // Full life bar at the start
    }

    private void Update()
    {
        // Update life bar based on player's current life
        currentLifeBar.fillAmount = playerLife.playerLife / 5f;  // Assuming max life is 5
    }
}