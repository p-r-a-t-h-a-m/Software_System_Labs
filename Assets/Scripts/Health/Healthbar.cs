using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float health;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        health = player.GetComponent<PlayerLife>().playerHealth;
        totalhealthBar.fillAmount = 1;
    }
    private void Update()
    {
        health = player.GetComponent<PlayerLife>().playerHealth;
        currenthealthBar.fillAmount = health / 100;
    }
}