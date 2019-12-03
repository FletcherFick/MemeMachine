using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerButton : MonoBehaviour
{
    public GameObject muteButton;
    public GameObject subtitlesButton;
    public GameObject exitButton;

    public void ToggleOptionsButtons()
    {
        if (muteButton.activeSelf)
        {
            muteButton.SetActive(false);
            subtitlesButton.SetActive(false);
            exitButton.SetActive(false);
        }
        else
        {
            muteButton.SetActive(true);
            subtitlesButton.SetActive(true);
            exitButton.SetActive(true);
        }
    }
}