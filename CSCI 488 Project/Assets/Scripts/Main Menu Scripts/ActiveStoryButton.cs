using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveStoryButton : MonoBehaviour
{
    public void StartActiveStory()
    {
        SceneManager.LoadScene("ActiveScene");
    }
}
