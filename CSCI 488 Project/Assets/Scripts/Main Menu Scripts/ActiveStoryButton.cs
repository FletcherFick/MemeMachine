using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveStoryButton : MonoBehaviour
{
    public GameObject confirmationWindow;
    public GameObject screenShade;
    public GameObject passiveStoryName;
    public GameObject passiveStoryStartButton;
    public GameObject activeStoryName;
    public GameObject activeStoryStartButton;

    public void StartActiveStory()
    {
        screenShade.SetActive(true);
        confirmationWindow.SetActive(true);
        passiveStoryName.SetActive(false);
        activeStoryName.SetActive(true);
        passiveStoryStartButton.SetActive(false);
        activeStoryStartButton.SetActive(true);
    }
}
