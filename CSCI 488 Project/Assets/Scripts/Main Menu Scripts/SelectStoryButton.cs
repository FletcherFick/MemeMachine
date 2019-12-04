using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStoryButton : MonoBehaviour
{
    private GameObject _menuTitle;
    private GameObject _selectStoryButton;
    private GameObject _settingsButton;
    private GameObject _quitButton;

    public GameObject storySelectTitle;
    public GameObject passiveStoryButton;
    public GameObject activeStoryButton;
    public GameObject returnButton;

    public Image transitionFade;
    public Animator animator;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _menuTitle = GameObject.Find("Main Menu Title");
        _selectStoryButton = GameObject.Find("Select Story Button");
        _settingsButton = GameObject.Find("Settings Button");
        _quitButton = GameObject.Find("Quit Button");
    }

    public void SelectStory()
    {
        StartCoroutine(SelectStoryTransition());
    }

    private IEnumerator SelectStoryTransition()
    {
        animator.speed = 4.0f;
        animator.SetBool("fade", true);
        yield return new WaitUntil(()=>transitionFade.color.a==1);

        _menuTitle.gameObject.SetActive(false);
        _selectStoryButton.gameObject.SetActive(false);
        _settingsButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
        storySelectTitle.SetActive(true);
        passiveStoryButton.SetActive(true);
        activeStoryButton.SetActive(true);
        returnButton.SetActive(true);

        animator.SetBool("fade", false);
        yield return new WaitUntil(()=>transitionFade.color.a==0);
        animator.speed = 1.0f;
    }
}
