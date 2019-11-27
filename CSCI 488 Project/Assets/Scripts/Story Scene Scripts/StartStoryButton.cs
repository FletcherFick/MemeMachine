using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StartStoryButton is dedicated to allowing the Start Story button
/// to clear the placement buttons from the UI.
///</summary>
public class StartStoryButton : MonoBehaviour
{
    public bool isPassiveScene;

    /// <summary>_buttonHandler references Button Manager.</summary>
    /// <value>
    /// _buttonHandler is used to store a reference to the Button 
    /// Manager game object.
    /// </value>
    private GameObject _buttonHandler;

    /// <summary>_buttonHandlerScript references the ButtonHandler script.</summary>
    /// <value>
    /// _buttonHandlerScript is used to reference the ButtonHandler script
    /// connected to the Button Manager game object.
    /// </value>
    private ButtonHandler _buttonHandlerScript;

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

    private GameObject _activeSceneHandler;
    private ActiveSceneHandler _activeSceneScript;

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

        /// Connect _buttonHandlerScript to the ButtonHandler script on _buttonHandler.
        _buttonHandlerScript = _buttonHandler.GetComponent<ButtonHandler>();

        if (isPassiveScene)
        {
            /// Connect _passiveSceneHandler to the Passive Scene Handler game object.
            _passiveSceneHandler = GameObject.Find("Passive Scene Handler");

            /// Connect _passiveSceneScript to the PassiveSceneHandler script on _passiveSceneHandler.
            _passiveSceneScript = _passiveSceneHandler.GetComponent<PassiveSceneHandler>();
        }
        else
        {
            _activeSceneHandler = GameObject.Find("Active Scene Handler");
            _activeSceneScript = _activeSceneHandler.GetComponent<ActiveSceneHandler>();
        }

        /// Create references to the components that render the preview model.
        _modelDisplayBackground = GameObject.Find("Model Display");
        _modelDisplayCamera = GameObject.Find("Object UI Render Camera");
        _modelDisplayRender = GameObject.Find("Object Display");
    }

    /// <summary>
    /// SetupStory is called when the Start Story button is clicked, and it calls
    /// the ClearObjectPlacementButtons function in the ButtonHandler script.
    /// </summary>
    public void SetupStory()
    {
        /// Make all object placement buttons inactive.
        _buttonHandlerScript.ClearObjectPlacementButtons();

        /// Set all components involved with the preview model to inactive.
        _modelDisplayBackground.SetActive(false);
        _modelDisplayCamera.SetActive(false);
        _modelDisplayRender.SetActive(false);

        /// Start the story.
        StartCoroutine(BeginStory(5));
    }

    /// <summary>
    /// BeginStory is used to call the StartStory method from PassiveSceneHandler.
    /// </summary>
    /// <param name="seconds">How long to wait until starting the story.</param>
    private IEnumerator BeginStory(int seconds)
    {
        /// Wait for X seconds.
        yield return new WaitForSeconds(seconds);

        if (isPassiveScene)
        {
            /// Call the StartStory function.
            _passiveSceneScript.StartStory();
        }
        else
        {
            _activeSceneScript.inactiveCrosshair.SetActive(true);
            _activeSceneScript.StartStory();
        }
    }
}