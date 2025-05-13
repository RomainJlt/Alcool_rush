using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Text controlsText;
    [SerializeField] private Text creditsText;

    private Resolution[] resolutions;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(CloseOptions);

        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }

        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        if (controlsText != null)
        {
            controlsText.text = "Contrôles :\nEspace = Sauter\nFlèches = Se déplacer\nEchap = Pause";
        }

        if (creditsText != null)
        {
            creditsText.text = "Map : https://jik-a-4.itch.io/metrocity\nCréateurs du jeu : https://github.com/CryptoDebug - https://github.com/RomainJlt - https://github.com/star0062 - https://github.com/PaulDecat";
        }
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Home");
    }

    private void OpenOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
        UpdateCreditsVisibility();
    }

    private void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void CloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        UpdateCreditsVisibility();
    }

    private void UpdateCreditsVisibility()
    {
        if (creditsText != null)
        {
            creditsText.gameObject.SetActive(optionsPanel.activeSelf);
        }
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (resolutionDropdown != null && resolutions != null && resolutions.Length > 0)
        {
            int index = resolutionDropdown.value;
            Resolution res = resolutions[index];
            Screen.SetResolution(res.width, res.height, isFullscreen);
        }
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}