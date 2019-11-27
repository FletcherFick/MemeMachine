using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ExitButton is used by the exit button to load the main menu scene.
/// </summary>
public class ExitButton : MonoBehaviour
{
    /// <summary>
    /// ExitScene is used to load the main menu scene.
    /// </summary>
    public void ExitScene()
    {
        SceneManager.LoadScene("TempMainMenu");
    }
}