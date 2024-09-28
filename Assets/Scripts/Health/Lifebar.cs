using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float life;
    [SerializeField] private Image totalLifeBar;
    [SerializeField] private Image currentLifeBar;

    private void Start()
    {
        life = player.GetComponent<PlayerLife>().playerLife;
        totalLifeBar.fillAmount = 1f;
    }
    private void Update()
    {
        life = player.GetComponent<PlayerLife>().playerLife;
        currentLifeBar.fillAmount = life / 5;
    }
}