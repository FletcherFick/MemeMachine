using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
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
    public GameObject storySelectTitle;
    public GameObject storyScrollView;

    private GameObject _settingsHandler;
    private SettingsHandler _settingsHandlerScript;

    public Image transitionFade;
    public Animator animator;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {

        _settingsHandler = GameObject.Find("Settings Handler");
        _settingsHandlerScript = _settingsHandler.GetComponent<SettingsHandler>();
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(MainMenuTransition());
    }

    private IEnumerator MainMenuTransition()
    {
        animator.speed = 4.0f;
        animator.SetBool("fade", true);
        yield return new WaitUntil(()=>transitionFade.color.a==1);

        menuTitle.gameObject.SetActive(true);
        selectStoryButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        _settingsHandlerScript._settingsMenuOpen = false;
        settingsTitle.gameObject.SetActive(false);
        muteButton.gameObject.SetActive(false);
        subtitlesButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        audioMutedText.SetActive(false);
        audioUnmutedText.SetActive(false);
        subtitlesEnabledText.SetActive(false);
        subtitlesDisabledText.SetActive(false);
        storySelectTitle.SetActive(false);
        storyScrollView.SetActive(false);

        animator.SetBool("fade", false);
        yield return new WaitUntil(()=>transitionFade.color.a==0);
        animator.speed = 1.0f;
    }
}
