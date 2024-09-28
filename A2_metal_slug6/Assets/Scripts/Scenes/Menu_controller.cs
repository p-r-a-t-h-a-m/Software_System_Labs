using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu_controller : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volume_text = null;
    [SerializeField] private Slider VolumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Graphics Settings")]

    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Levels To Load")]
    [SerializeField] private string newGameSceneName;
    private string savedGameSceneName;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Resolution Dropdowns")]
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

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

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volume_text.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());

    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        // Change your brightness with your post processing or whatever it is

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullscreen ? 1 : 0));
        Screen.fullScreen = _isFullscreen;

        StartCoroutine(ConfirmationBox());

    }

    public void ResetButton(string MenuType)
    {

        if (MenuType == "Graphics")
        {
            // Reset brightness value
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);

            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();

        }

        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            VolumeSlider.value = defaultVolume;
            volume_text.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);

    }
}

