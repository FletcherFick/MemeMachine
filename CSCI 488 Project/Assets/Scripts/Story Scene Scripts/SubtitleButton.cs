using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleButton : MonoBehaviour
{
    private GameObject _persistentSettings;
    private PersistentSettings _settingsScript;

    public GameObject subtitlesOnIcon;
    public GameObject subtitlesOffIcon;

    // Start is called before the first frame update
    void Start()
    {
        _persistentSettings = GameObject.Find("Persistent Settings");
        _settingsScript = _persistentSettings.GetComponent<PersistentSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_settingsScript.GetSubtitleStatus())
        {
            subtitlesOnIcon.SetActive(true);
            subtitlesOffIcon.SetActive(false);
        }
        else
        {
            subtitlesOnIcon.SetActive(false);
            subtitlesOffIcon.SetActive(true);
        }
    }

    public void UpdateSubtitleSetting()
    {
        if (_settingsScript.GetSubtitleStatus() == true)
        {
            _settingsScript.SetSubtitleStatus(false);
        }
        else{
            _settingsScript.SetSubtitleStatus(true);
        }
    }
}
