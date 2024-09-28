using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    private PlayerLife playerLife;

    private void Start()
    {
        // Safely assign the PlayerLife component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerLife = player.GetComponent<PlayerLife>();
        }
        else
        {
            Debug.LogError("Player object not found.");
        }
    }

    void Update()
    {
        // Check if player's life is zero or less and trigger EndGame with a delay
        if (playerLife != null && playerLife.playerLife <= 0)
        {
            StartCoroutine(EndGameRoutine());
            return;
        }

        // Prevent pausing if player is dead, otherwise check for pause input
        if (playerLife != null && playerLife.playerLife > 0 && Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Resume the game
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
        isPaused = true;
    }

    // Coroutine to handle delayed transition to EndGame scene
    IEnumerator EndGameRoutine()
    {
        // Optional delay before switching to the "EndGame" scene
        yield return new WaitForSeconds(2f);  
        SceneManager.LoadScene("EndGame");
    }

    public void StartMenuButton()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Resume game time before switching scenes
        SceneManager.LoadScene("Menu");
    }

    public void QuitGameButton()
    {
        Debug.Log("Quitting game...");
        Application.Quit();  // Quit the application
    }
}
