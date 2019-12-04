using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Image transitionFade;
    public Animator animator;

    public void QuitApplication()
    {
        StartCoroutine(QuitTransition());
    }

    private IEnumerator QuitTransition()
    {
        animator.speed = 4.0f;
        animator.SetBool("fade", true);
        yield return new WaitUntil(()=>transitionFade.color.a==1);

        Application.Quit();
    }
}
