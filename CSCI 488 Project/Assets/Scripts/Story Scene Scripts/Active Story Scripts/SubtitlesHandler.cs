using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesHandler : MonoBehaviour
{
    private GameObject _persistentSettings;
    private PersistentSettings _settingsScript;

    public List<GameObject> subtitles;
    public static bool canShowSubtitles = true;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _persistentSettings = GameObject.Find("Persistent Settings");
        _settingsScript = _persistentSettings.GetComponent<PersistentSettings>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_settingsScript.GetSubtitleStatus())
        {
            if (ActiveSceneHandler.interactedWithRadio && canShowSubtitles)
            {
                StartCoroutine(DisplayRadioSubtitles());
                canShowSubtitles = false;
            }
        }
    }

    private IEnumerator DisplayRadioSubtitles()
    {
        subtitles[0].SetActive(true);
        yield return new WaitForSeconds(3.75f);

        subtitles[0].SetActive(false);
        subtitles[1].SetActive(true);
        yield return new WaitForSeconds(2.5f);

        subtitles[1].SetActive(false);
        subtitles[2].SetActive(true);
        yield return new WaitForSeconds(3.5f);

        subtitles[2].SetActive(false);
        subtitles[3].SetActive(true);
        yield return new WaitForSeconds(4.0f);

        subtitles[3].SetActive(false);
        subtitles[4].SetActive(true);
        yield return new WaitForSeconds(4.0f);

        subtitles[4].SetActive(false);
        subtitles[5].SetActive(true);
        yield return new WaitForSeconds(3.0f);

        subtitles[5].SetActive(false);
        subtitles[6].SetActive(true);
        yield return new WaitForSeconds(3.0f);

        subtitles[6].SetActive(false);
        subtitles[7].SetActive(true);
        yield return new WaitForSeconds(3.0f);

        subtitles[7].SetActive(false);
        subtitles[8].SetActive(true);
        yield return new WaitForSeconds(3.0f);

        subtitles[8].SetActive(false);
        subtitles[9].SetActive(true);
        yield return new WaitForSeconds(3.5f);

        subtitles[9].SetActive(false);
    }
}
