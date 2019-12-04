using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassiveStoryButton : MonoBehaviour
{
    public Image transitionFade;
    public Animator animator;

    public void StartPassiveStory()
    {
        StartCoroutine(FadeOutTransition());
    }

    private IEnumerator FadeOutTransition()
    {
        animator.SetBool("fade", true);
        yield return new WaitUntil(()=>transitionFade.color.a==1);
        SceneManager.LoadScene("PassiveScene");
    }
}
