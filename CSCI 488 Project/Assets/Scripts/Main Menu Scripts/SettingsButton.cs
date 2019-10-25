using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject menuTitle;
    public GameObject selectStoryButton;
    public GameObject settingsButton;
    public GameObject quitButton;
    public GameObject settingsTitle;
    public GameObject muteButton;
    public GameObject subtitlesButton;
    public GameObject returnButton;
    public GameObject audioMutedText;
    public GameObject audioUnmutedText;
    public GameObject subtitlesEnabledText;
    public GameObject subtitlesDisabledText;

    private GameObject _settingsHandler;
    private SettingsHandler _settingsHandlerScript;

    public GameObject persistentSettings;
    private PersistentSettings _settingsScript;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _settingsHandler = GameObject.Find("Settings Handler");
        _settingsHandlerScript = _settingsHandler.GetComponent<SettingsHandler>();
        _settingsScript = persistentSettings.GetComponent<PersistentSettings>();
    }

    public void OpenSettings()
    {
        menuTitle.gameObject.SetActive(false);
        selectStoryButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        settingsTitle.gameObject.SetActive(true);
        muteButton.gameObject.SetActive(true);
        subtitlesButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);

        if (_settingsScript.GetMuteStatus())
        {
            audioMutedText.SetActive(true);
            audioUnmutedText.SetActive(false);
        }
        else
        {
            audioMutedText.SetActive(false);
            audioUnmutedText.SetActive(true);
        }

        if (_settingsScript.GetSubtitleStatus())
        {
            subtitlesEnabledText.SetActive(true);
            subtitlesDisabledText.SetActive(false);
        }
        else
        {
            subtitlesEnabledText.SetActive(false);
            subtitlesDisabledText.SetActive(true);
        }
        
        _settingsHandlerScript._settingsMenuOpen = true;
    }
}
