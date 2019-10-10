using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlaceObjectButton is dedicated to allowing the Place Object button
/// to place an object when it is clicked.
///</summary>
public class PlaceObjectButton : MonoBehaviour
{
    /// <summary>interactionManager references Interaction Manager.</summary>
    /// <value>
    /// interactionManager is used to store a referene to the Interaction 
    /// Manager game object.
    /// </value>
    private GameObject interactionManager;

    /// <summary>scriptReference references the ARTapToPlaceObject script.</summary>
    /// <value>
    /// scriptReference is used to reference the ARTapToPlaceObject script
    /// connected to the Interaction Manager game object.
    /// </value>
    private ARTapToPlaceObject scriptReference;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        /// Connect interactionManager to the Interaction Manager game object.
        interactionManager = GameObject.Find("Interaction Manager");

        /// Connect scriptReference to the ARTapToPlace script on interactionManager.
        scriptReference = interactionManager.GetComponent<ARTapToPlaceObject>();
    }

    /// <summary>
    /// PlaceNewObjeect is called when the Place Object button is clicked, and
    /// it calls the PlaceObject function in the ARTapToPlaceObject script.
    /// </summary>
    public void PlaceNewObject()
    {
        /// Attempt to place the indicated game object.
        scriptReference.PlaceObject(scriptReference.objectToPlace);
    }
}