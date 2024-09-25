using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Life : MonoBehaviour
{
    [SerializeField] private Player_Health player_Health;
    [SerializeField] private Image _totalLife;
    [SerializeField] private Image _currentLife;

    private void Start()
    {
        _totalLife.fillAmount = player_Health.currentLife / 10;
    }
    private void Update()
    {
        _currentLife.fillAmount = player_Health.currentLife / 10;
    }
}
