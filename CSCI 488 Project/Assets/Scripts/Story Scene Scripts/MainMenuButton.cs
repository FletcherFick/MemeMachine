using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// MainMenuButton allows any Main Menu button to return the user to the
/// main menu scene when pressed.
/// </summary>
public class MainMenuButton : MonoBehaviour
{
    /// <summary>
    /// LoadMainMenu is used to load the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("TempMainMenu");
    }
}