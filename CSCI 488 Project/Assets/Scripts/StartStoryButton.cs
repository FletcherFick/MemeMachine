using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StartStoryButton is dedicated to allowing the Start Story button
/// to clear the placement buttons from the UI.
///</summary>
public class StartStoryButton : MonoBehaviour
{
    /// <summary>_buttonHandler references Button Manager.</summary>
    /// <value>
    /// _buttonHandler is used to store a reference to the Button 
    /// Manager game object.
    /// </value>
    private GameObject _buttonHandler;

    /// <summary>_scriptReference references the ButtonHandler script.</summary>
    /// <value>
    /// _scriptReference is used to reference the ButtonHandler script
    /// connected to the Button Manager game object.
    /// </value>
    private ButtonHandler _scriptReference;

    /// <summary>_modelDisplayBackground references the Object Display object.</summary>
    /// <value>
    /// _modelDisplayBackground is used to reference the Object Display object
    /// stored under the UI canvas.
    /// </value>
    private GameObject _modelDisplayBackground;

    /// <summary>_modelDisplayCamera references the camera that renders the model.</summary>
    /// <value>
    /// _modelDisplayCamera is used to reference Object UI Render Camera stored
    /// under the UI Handler.
    /// </value>
    private GameObject _modelDisplayCamera;

    /// <summary>_modelDisplayRender references the Model Display object.</summary>
    /// <value>
    /// _modelDisplayRender is used to reference the Model Display object
    /// stored under the UI handler.
    /// </value>
    private GameObject _modelDisplayRender;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        /// Connect _buttonHandler to the Button Manager game object.
        _buttonHandler = GameObject.Find("Button Handler");

        /// Create references to the components that render the preview model.
        _modelDisplayBackground = GameObject.Find("Model Display");
        _modelDisplayCamera = GameObject.Find("Object UI Render Camera");
        _modelDisplayRender = GameObject.Find("Object Display");

        /// Connect _scriptReference to the ButtonHandler script on _buttonHandler.
        _scriptReference = _buttonHandler.GetComponent<ButtonHandler>();
    }

    /// <summary>
    /// StartStory is called when the Start Story button is clicked, and it calls
    /// the ClearObjectPlacementButtons function in the ButtonHandler script.
    /// </summary>
    public void StartStory()
    {
        /// Make all object placement buttons inactive.
        _scriptReference.ClearObjectPlacementButtons();

        /// Set all components involved with the preview model to inactive.
        _modelDisplayBackground.SetActive(false);
        _modelDisplayCamera.SetActive(false);
        _modelDisplayRender.SetActive(false);
    }
}