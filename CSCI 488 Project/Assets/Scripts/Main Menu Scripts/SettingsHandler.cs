using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public GameObject muteButton;
    public GameObject subtitlesButton;

    public GameObject persistentSettings;
    private PersistentSettings _settingsScript;

    public bool _settingsMenuOpen;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _settingsScript = persistentSettings.GetComponent<PersistentSettings>();
        _settingsMenuOpen = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (_settingsMenuOpen)
        {
            if (!_settingsScript.GetMuteStatus())
            {
                muteButton.GetComponent<Image>().color = new Color32(255, 167, 167, 255);
            }
            else
            {
                muteButton.GetComponent<Image>().color = new Color32(165, 255, 153, 255);
            }

            if (!_settingsScript.GetSubtitleStatus())
            {
                subtitlesButton.GetComponent<Image>().color = new Color32(255, 167, 167, 255);
            }
            else
            {
                subtitlesButton.GetComponent<Image>().color = new Color32(165, 255, 153, 255);
            }
        }
    }
}
