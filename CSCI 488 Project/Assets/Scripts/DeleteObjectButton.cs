using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DeleteObjectButton is dedicated to allowing the Undo button
/// to delete the placed object when it is clicked.
///</summary>
public class DeleteObjectButton : MonoBehaviour
{
    /// <summary>_interactionHandler references Interaction Manager.</summary>
    /// <value>
    /// _interactionHandler is used to store a reference to the Interaction 
    /// Manager game object.
    /// </value>
    private GameObject _interactionHandler;

    /// <summary>_scriptReference references the InteractionHandler script.</summary>
    /// <value>
    /// _scriptReference is used to reference the InteractionHandler script
    /// connected to the Interaction Manager game object.
    /// </value>
    private InteractionHandler _scriptReference;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        /// Connect _interactionHandler to the Interaction Manager game object.
        _interactionHandler = GameObject.Find("Interaction Handler");

        /// Connect _scriptReference to the InteractionHandler script on _interactionHandler.
        _scriptReference = _interactionHandler.GetComponent<InteractionHandler>();
    }

    /// <summary>
    /// DeleteObject is called when the Delete Object button is clicked, and
    /// it calls the DeledPlacedObject function in the InteractionHandler script.
    /// </summary>
    public void DeleteObject()
    {
        /// Attempt to place the indicated game object.
        _scriptReference.DeletePlacedObject();
    }
}
