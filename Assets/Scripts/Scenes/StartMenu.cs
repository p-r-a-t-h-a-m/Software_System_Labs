using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Load next scene after the startmenu in build settings
    public void StartWelcome()
    {
        StartCoroutine(LoadSceneWithDelay("Welcome"));
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Welcome")
        {
            StartCoroutine(LoadSceneWithDelay("Main_Menu"));
        }
    }

    public void StartMenuButton()
    {
        StartCoroutine(LoadSceneWithDelay("Main_Menu"));
    }

    public void StartPlay1()
    {
        StartCoroutine(LoadSceneWithDelay("Level1"));
    }

    public void StartPlay2()
    {
        StartCoroutine(LoadSceneWithDelay("Level2"));
    }

    public void StartPlay3()
    {
        StartCoroutine(LoadSceneWithDelay("Level3"));
    }

    public void Gamefinished()
    {
        StartCoroutine(LoadSceneWithDelay("EndGame"));
    }

    // Quit the application by pressing the quit button
    public void QuitPlay()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }

    // Coroutine to load scene with a 3-second delay
    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);  // Wait for 3 seconds
        SceneManager.LoadScene(sceneName);    // Load the scene
    }
}
