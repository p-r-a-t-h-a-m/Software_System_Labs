using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_controller : MonoBehaviour
{
    [Header("Levels To Load")]
    [SerializeField] private string newGameSceneName;
    private string savedGameSceneName;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameSceneName);
    }

    public void LoadSavedGame()
    {
        if (PlayerPrefs.HasKey("Level1"))
        {
            savedGameSceneName = PlayerPrefs.GetString("Level1");
            SceneManager.LoadScene(savedGameSceneName);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

