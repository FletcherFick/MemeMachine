using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    private GameObject _settingsHandler;
    private SettingsHandler _settingsHandlerScript;

    public GameObject audioMutedText;
    public GameObject audioUnmutedText;

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

    public void ChangeMuteSetting()
    {
        if (_settingsScript.GetMuteStatus())
        {
            _settingsScript.SetMuteStatus(false);
            audioMutedText.gameObject.SetActive(false);
            audioUnmutedText.gameObject.SetActive(true);
        }
        else
        {
            _settingsScript.SetMuteStatus(true);
            audioMutedText.gameObject.SetActive(true);
            audioUnmutedText.gameObject.SetActive(false);
        }
    }
}
