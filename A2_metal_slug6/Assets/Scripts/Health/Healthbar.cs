using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerLife playerLife;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        // Cache the PlayerLife component reference
        playerLife = player.GetComponent<PlayerLife>();
        totalhealthBar.fillAmount = 1f;  // Start with full health
    }

    private void Update()
    {
        // Update the health bar based on the player's health
        currenthealthBar.fillAmount = playerLife.playerHealth / 100f;
    }
}
