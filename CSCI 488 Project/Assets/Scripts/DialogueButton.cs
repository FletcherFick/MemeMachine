using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DialogueButton is used by the dialogue button to inform PassiveSceneHandler
/// it has been tapped.
///</summary>
public class DialogueButton : MonoBehaviour
{
    /// <summary>_passiveSceneHandler references Passive Scene Handler.</summary>
    /// <value>
    /// _passiveSceneHandler is used to store a reference to the Passive 
    /// Scene Handler game object.
    /// </value>
    private GameObject _passiveSceneHandler;

    /// <summary>_passiveSceneScript references the PassiveSceneHandler script.</summary>
    /// <value>
    /// _passiveSceneScript is used to reference the PassiveSceneHandler script
    /// connected to the Passive Scene Handler game object.
    /// </value>
    private PassiveSceneHandler _passiveSceneScript;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        /// Connect _passiveSceneHandler to the Passive Scene Handler game object.
        _passiveSceneHandler = GameObject.Find("Passive Scene Handler");

        /// Connect _passiveSceneScript to the PassiveSceneHandler script on _passiveSceneHandler.
        _passiveSceneScript = _passiveSceneHandler.GetComponent<PassiveSceneHandler>();
    }

    /// <summary>
    /// DialogueClicked is used to tell the PassiveSceneHandler script
    /// that the dialogue button was clicked.
    /// </summary>
    public void DialogueClicked()
    {
        _passiveSceneScript.ActivateDialogue();
    }
}
