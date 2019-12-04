using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// MainMenuButton allows any Main Menu button to return the user to the
/// main menu scene when pressed.
/// </summary>
public class MainMenuButton : MonoBehaviour
{
    public Image transitionFade;
    public Animator animator;

    /// <summary>
    /// LoadMainMenu is used to load the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        StartCoroutine(FadeOutTransition());
    }

    private IEnumerator FadeOutTransition()
    {
        animator.SetBool("fade", true);
        yield return new WaitUntil(()=>transitionFade.color.a==1);
        SceneManager.LoadScene("TempMainMenu");
    }
}