using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitConfirmation : MonoBehaviour
{
    public Image transitionFade;
    public Animator animator;

    /// <summary>
    /// ExitScene is used to load the main menu scene.
    /// </summary>
    public void ExitScene()
    {
        Time.timeScale = 1.0f;

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "ActiveScene")
        {
            GameObject.Find("Active Scene Handler").GetComponent<ActiveSceneHandler>().ResetTriggerValues();
        }
        
        StartCoroutine(FadeOutTransition());
    }

    private IEnumerator FadeOutTransition()
    {
        animator.SetBool("fade", true);
        yield return new WaitUntil(()=>transitionFade.color.a==1);
        SceneManager.LoadScene("TempMainMenu");
    }
}
