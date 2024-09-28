using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method will be called when the Start Game button is pressed
    public void StartGame()
    {
        // Load the next scene, assuming your game scene is indexed at 1 in the Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // This method will be called when the Exit Game button is pressed
    public void ExitGame()
    {
        // This will only work in the built version, not in the Unity editor
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
