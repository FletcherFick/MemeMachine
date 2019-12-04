using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public Image transitionFade;
    public Animator animator;
    public GameObject passiveStoryStartButton;
    public GameObject activeStoryStartButton;

    public void StartStory()
    {
        StartCoroutine(FadeOutTransition());
    }

    private IEnumerator FadeOutTransition()
    {
        animator.SetBool("fade", true);
        yield return new WaitUntil(()=>transitionFade.color.a==1);

        if (passiveStoryStartButton.activeSelf)
        {
            SceneManager.LoadScene("PassiveScene");
        }
        else
        {
            SceneManager.LoadScene("ActiveScene");
        }
    }
}