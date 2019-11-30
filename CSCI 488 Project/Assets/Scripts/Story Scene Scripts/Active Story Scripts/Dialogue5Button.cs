using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue5Button : MonoBehaviour
{
    private GameObject activeSceneHandler;
    private ActiveSceneHandler activeSceneScript;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        activeSceneHandler = GameObject.Find("Active Scene Handler");
        activeSceneScript = activeSceneHandler.GetComponent<ActiveSceneHandler>();
    }

    public void ActivateSarahAudio()
    {
        ActiveSceneHandler.sarahAudioTriggers[8] = true;
        activeSceneScript.DisableTransitionDialogue();
    }
}
