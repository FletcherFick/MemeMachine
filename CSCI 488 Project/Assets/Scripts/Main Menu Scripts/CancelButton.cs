using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CancelButton : MonoBehaviour
{
    public GameObject confirmationWindow;
    public GameObject screenShade;

    public void Cancel()
    {
        if (SceneManager.GetActiveScene().name != "TempMainMenu")
        {
            Time.timeScale = 1.0f;
        }
        
        confirmationWindow.SetActive(false);
        screenShade.SetActive(false);
    }
}
