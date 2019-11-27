using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ActiveSceneHandler : MonoBehaviour
{
    public GameObject activeCrosshair;
    public GameObject inactiveCrosshair;

    /// <summary>_storyStarted determines if the story has started.</summary>
    /// <value>
    /// _storyStarted keeps track of if the story is started; it is instantiated
    /// as false and turns true when the Start Story button is tapped.
    /// </value>
    private bool _storyStarted;

    /// <summary>_arRaycastManager references the scene's ray manager.</summary>
    /// <value>
    /// _arRaycastManager references and is used to call the 
    /// AR Session Origin's AR Raycast Manager component.
    /// </value>
    private ARRaycastManager _arRaycastManager;

    /// <summary>interactionHandler references Interaction Hanager.</summary>
    /// <value>
    /// _interactionHandler is used to store a reference to the Interaction 
    /// Handler game object.
    /// </value>
    private GameObject _interactionHandler;

    /// <summary>_interactionScript references the InteractionHandler script.</summary>
    /// <value>
    /// _interactionScript is used to reference the InteractionHandler script
    /// connected to the Interaction Handler game object.
    /// </value>
    private InteractionHandler _interactionScript;

    // Start is called before the first frame update
    void Start()
    {
        _storyStarted = false;
        activeCrosshair.SetActive(false);
        inactiveCrosshair.SetActive(false);

        /// Connect _arRaycastManager to the AR Session Origin's raycast manager.
        _arRaycastManager = FindObjectOfType<ARSessionOrigin>().
            GetComponent<ARRaycastManager>();

        /// Connect _interactionHandler to the Interaction Manager game object.
        _interactionHandler = GameObject.Find("Interaction Handler");

        /// Connect _interactionScript to the ARTapToPlace script on interactionManager.
        _interactionScript = _interactionHandler.GetComponent<InteractionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_storyStarted)
        {
            // Determine where the center of the screen is.
            Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, Camera.main.nearClipPlane));
            
            RaycastHit hit;

            if (Physics.Raycast(screenCenter, Camera.main.transform.forward, out hit, 10000.0f))
            {
                if (hit.transform.gameObject.tag == "InteriorHandle")
                {
                    _interactionScript.GetPlacedObjectAnimator().SetBool("openDoor", true);
                    _interactionScript.GetPlacedObjectAnimator().SetBool("closeDoor", false);
                }

                if (hit.transform.gameObject.tag == "ExteriorHandle")
                {
                    _interactionScript.GetPlacedObjectAnimator().SetBool("openDoor", false);
                    _interactionScript.GetPlacedObjectAnimator().SetBool("closeDoor", true);
                }
            }
        }
    }

    /// <summary>
    /// StartStory is used by other classes to set _storyStarted to true.
    /// </summary>
    public void StartStory()
    {
        _storyStarted = true;
    }
}
