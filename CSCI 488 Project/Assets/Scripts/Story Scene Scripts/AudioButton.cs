using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    private GameObject _persistentSettings;
    private PersistentSettings _settingsScript;

    public GameObject audioOnIcon;
    public GameObject audioOffIcon;

    // Start is called before the first frame update
    void Start()
    {
        _persistentSettings = GameObject.Find("Persistent Settings");
        _settingsScript = _persistentSettings.GetComponent<PersistentSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_settingsScript.GetMuteStatus())
        {
            audioOnIcon.SetActive(false);
            audioOffIcon.SetActive(true);
        }
        else
        {
            audioOnIcon.SetActive(true);
            audioOffIcon.SetActive(false);
        }
    }

    public void UpdateAudioSetting()
    {
        if (_settingsScript.GetMuteStatus() == true)
        {
            _settingsScript.SetMuteStatus(false);
        }
        else{
            _settingsScript.SetMuteStatus(true);
        }
    }
}
