using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(player.GetComponent<PlayerLife>().playerLife <=0 )
        {
            SceneManager.LoadScene("EndGame");
        }

     if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();

            }

        }
     
    }
    public void ResumeGame()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void StartMenuButton()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
 
}
