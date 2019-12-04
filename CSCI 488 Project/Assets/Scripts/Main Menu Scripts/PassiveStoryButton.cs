using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStoryButton : MonoBehaviour
{
    public GameObject confirmationWindow;
    public GameObject screenShade;
    public GameObject passiveStoryName;
    public GameObject passiveStoryStartButton;
    public GameObject activeStoryName;
    public GameObject activeStoryStartButton;

    public void StartPassiveStory()
    {
        screenShade.SetActive(true);
        confirmationWindow.SetActive(true);
        passiveStoryName.SetActive(true);
        activeStoryName.SetActive(false);
        passiveStoryStartButton.SetActive(true);
        activeStoryStartButton.SetActive(false);
    }
}
