using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ExitButton is used by the exit button to load the main menu scene.
/// </summary>
public class ExitButton : MonoBehaviour
{
    public GameObject screenShade;
    public GameObject confirmationMenu;

    /// <summary>
    /// ExitScene is used to load the main menu scene.
    /// </summary>
    public void ExitScene()
    {
        Time.timeScale = 0.0f;

        screenShade.SetActive(true);
        confirmationMenu.SetActive(true);
    }
}