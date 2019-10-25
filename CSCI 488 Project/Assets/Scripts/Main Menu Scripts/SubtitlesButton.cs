using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesButton : MonoBehaviour
{
    private GameObject _settingsHandler;
    private SettingsHandler _settingsHandlerScript;

    public GameObject subtitlesEnabledText;
    public GameObject subtitlesDisabledText;

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

    public void ChangeSubtitlesSetting()
    {
        if (_settingsScript.GetSubtitleStatus())
        {
            _settingsScript.SetSubtitleStatus(false);
            subtitlesDisabledText.SetActive(true);
            subtitlesEnabledText.SetActive(false);
        }
        else
        {
            _settingsScript.SetSubtitleStatus(true);
            subtitlesDisabledText.SetActive(false);
            subtitlesEnabledText.SetActive(true);
        }
    }
}
