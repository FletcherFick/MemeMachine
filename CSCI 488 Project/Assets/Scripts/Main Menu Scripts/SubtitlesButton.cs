using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesButton : MonoBehaviour
{
    private GameObject _settingsHandler;
    private SettingsHandler _settingsHandlerScript;

    public GameObject subtitlesEnabledText;
    public GameObject subtitlesDisabledText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _settingsHandler = GameObject.Find("Settings Handler");
        _settingsHandlerScript = _settingsHandler.GetComponent<SettingsHandler>();
    }

    public void ChangeSubtitlesSetting()
    {
        if (_settingsHandlerScript._enableSubtitles)
        {
            _settingsHandlerScript._enableSubtitles = false;
            subtitlesDisabledText.SetActive(true);
            subtitlesEnabledText.SetActive(false);
        }
        else
        {
            _settingsHandlerScript._enableSubtitles = true;
            subtitlesDisabledText.SetActive(false);
            subtitlesEnabledText.SetActive(true);
        }
    }
}
