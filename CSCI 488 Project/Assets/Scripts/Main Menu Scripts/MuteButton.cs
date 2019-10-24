using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    private GameObject _settingsHandler;
    private SettingsHandler _settingsHandlerScript;

    public GameObject audioMutedText;
    public GameObject audioUnmutedText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _settingsHandler = GameObject.Find("Settings Handler");
        _settingsHandlerScript = _settingsHandler.GetComponent<SettingsHandler>();
    }

    public void ChangeMuteSetting()
    {
        if (_settingsHandlerScript._muteAudio)
        {
            _settingsHandlerScript._muteAudio = false;
            audioMutedText.gameObject.SetActive(false);
            audioUnmutedText.gameObject.SetActive(true);
        }
        else
        {
            _settingsHandlerScript._muteAudio = true;
            audioMutedText.gameObject.SetActive(true);
            audioUnmutedText.gameObject.SetActive(false);
        }
    }
}
