using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassiveStoryButton : MonoBehaviour
{
    public void StartPassiveStory()
    {
        SceneManager.LoadScene("PassiveScene");
    }
}
