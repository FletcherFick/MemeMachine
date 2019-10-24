using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public GameObject muteButton;
    public GameObject subtitlesButton;

    public bool _muteAudio;
    public bool _enableSubtitles;

    public bool _settingsMenuOpen;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _muteAudio = false;
        _enableSubtitles = false;

        _settingsMenuOpen = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (_settingsMenuOpen)
        {
            if (!_muteAudio)
            {
                muteButton.GetComponent<Image>().color = Color.red;
            }
            else
            {
                muteButton.GetComponent<Image>().color = Color.green;
            }

            if (!_enableSubtitles)
            {
                subtitlesButton.GetComponent<Image>().color = Color.red;
            }
            else
            {
                subtitlesButton.GetComponent<Image>().color = Color.green;
            }
        }
    }
}
