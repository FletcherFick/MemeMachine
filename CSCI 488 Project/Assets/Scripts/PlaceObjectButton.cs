using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlaceObjectButton is dedicated to allowing the Place Object button
/// to place an object when it is clicked.
///</summary>
public class PlaceObjectButton : MonoBehaviour
{
    /// <summary>_interactionHandler references Interaction Manager.</summary>
    /// <value>
    /// _interactionHandler is used to store a referene to the Interaction 
    /// Manager game object.
    /// </value>
    private GameObject _interactionHandler;

    /// <summary>_interactionHandlerScript references the InteractionHandler script.</summary>
    /// <value>
    /// _interactionHandlerScript is used to reference the InteractionHandler script
    /// connected to the Interaction Manager game object.
    /// </value>
    private InteractionHandler _interactionHandlerScript;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        /// Connect _interactionHandler to the Interaction Manager game object.
        _interactionHandler = GameObject.Find("Interaction Handler");

        /// Connect _interactionHandlerScript to the ARTapToPlace script on _interactionHandler.
        _interactionHandlerScript = _interactionHandler.GetComponent<InteractionHandler>();
    }

    /// <summary>
    /// PlaceNewObjeect is called when the Place Object button is clicked, and
    /// it calls the PlaceObject function in the InteractionHandler script.
    /// </summary>
    public void PlaceNewObject()
    {
        /// Attempt to place the indicated game object.
        _interactionHandlerScript.PlaceObject(_interactionHandlerScript.objectToPlace);
    }
}