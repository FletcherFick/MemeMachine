using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

/// <summary>
/// InteractionHandler is used to allow the player to see the placement
/// reticle and lets them place an object at its position.
///</summary>
public class InteractionHandler : MonoBehaviour
{
    public bool isPassiveScene;

    public GameObject activeModelPreview;

    /// <summary>placementIndicator represents the placement reticle.</summary>
    /// <value>
    /// placementIndicator references and stores information about 
    /// the Placement Indicator game object.
    /// </value>
    public GameObject placementIndicator;

    /// <summary>objectToPlace represents the object being placed.</summary>
    /// <value>
    /// objectToPlace references the Game Piece game object prefab.
    /// </value>
    public GameObject objectToPlace;

    /// <summary>_arRaycastManager references the scene's ray manager.</summary>
    /// <value>
    /// _arRaycastManager references and is used to call the 
    /// AR Session Origin's AR Raycast Manager component.
    /// </value>
    private ARRaycastManager _arRaycastManager;

    /// <summary>_placementPose represents the reticle's transformation.</summary>
    /// <value>
    /// _placementPose is used to store both the position and 
    /// rotation of the placement reticle.
    /// </value>
    private Pose _placementPose;

    /// <summary>_placementPoseIsValid represents the ground's flatness.</summary>
    /// <value>
    /// _placementPoseIsValid is used to determine if the ray being emitted
    /// from the center of the screen is on a flat plane.
    /// </value>
    private bool _placementPoseIsValid;

    /// <summary>_rotationSpeed is how fast the placement reticle rotates.</summary>
    /// <value>
    /// _rotationSpeed is used to set how fast the placement indicator rotates;
    /// the higher the value, the faster the rotation.
    /// </value>
    private float _rotationSpeed;

    /// <summary>rotationPointerDown determines if a rotate button is down.</summary>
    /// <value>
    /// rotationPointerDown is used to determine if either the left or right
    /// object rotation buttons are being pressed.
    /// </value>
    private bool _rotationPointerDown;

    /// <summary>rotationDirection determines how to rotate the reticle.</summary>
    /// <value>
    /// rotationDirection is used to determine which direction to rotate
    /// the placement reticle; true is right, false is left.
    /// </value>
    private bool _rotationDirection;

    /// <summary>_placedObjectCount tracks the amount of objects placed.</summary>
    /// <value>
    /// _placedObjectCount is used to keep track of the amount of objects
    /// placed in the scene by the user.
    /// </value>
    private int _placedObjectCount;

    /// <summary>_placedGameObject refers to the object placed in the scene.</summary>
    /// <value>
    /// _placedGameObject is a reference to the game object that the player
    /// places in the scene and is used to manipulate it.
    /// </value>
    private GameObject _placedGameObject;

    /// <summary>_buttonHandler is a reference to ButtonHandler.</summary>
    /// <value>
    /// _buttonHandler is used to reference the ButtonHandler script on the 
    /// Button Handler game object.
    /// </value>
    private GameObject _buttonHandler;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (isPassiveScene)
        {
            /// Setup some variables for later use.
            _placementPoseIsValid = false;
            _rotationSpeed = 100.0f;
            _placedObjectCount = 0;

            /// Connect _arRaycastManager to the AR Session Origin's raycast manager.
            _arRaycastManager = FindObjectOfType<ARSessionOrigin>().
                GetComponent<ARRaycastManager>();

            /// Set the initial rotation for the placement reticle.
            placementIndicator.transform.rotation = Quaternion.Euler(Vector3.forward);
        }
        
        /// Connect _buttonHandler to the ButtonHandler script.
        _buttonHandler = GameObject.Find("Button Handler");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isPassiveScene)
        {
            /// Update the pose of the placement reticle.
            UpdatePlacementPose();

            /// Update the placement reticle game object's position.
            UpdatePlacementIndicator();

            /// If a rotation button is being pushed, rotate the placement reticle.
            if (_rotationPointerDown && _placedObjectCount == 0)
            {
                RotatePlacementIndicator(_rotationDirection);
            }
        }
    }

    /// <summary>
    /// UpdatePlacementPose is used to determine where objects will be placed
    /// in the scene, as well as their rotation.
    /// </summary>
    private void UpdatePlacementPose()
    {
        // Determine where the center of the screen is.
        Vector3 screenCenter =
            Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        /// Create a list of AR Raycast Hits.
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        /// If the ray being emitted from the center of the screen collides
        /// with a plane, add a hit to the hits list.
        _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        /// If there is a hit in the hits list, use the first hit in the list
        /// to determine the placement reticle's new pose.
        _placementPoseIsValid = hits.Count > 0;
        if (_placementPoseIsValid)
        {
            _placementPose = hits[0].pose;
        }
    }

    /// <summary>
    /// UpdatePlacementIndicator is used to enable and disable the placement
    /// reticle, depending on if it's in a valid space.
    /// </summary>
    private void UpdatePlacementIndicator()
    {
        /// If the placement reticle's pose is valid, display the reticle.
        if (_placementPoseIsValid && _placedObjectCount == 0)
        {
            /// Display the placement reticle.
            placementIndicator.SetActive(true);

            /// Update the placement reticle's position.
            placementIndicator.transform.position = _placementPose.position;
        }
        else
        {
            /// Hide the placement reticle.
            placementIndicator.SetActive(false);
        }
    }

    /// <summary>
    /// PlaceObject is used to create a new object at the placement reticle.
    /// </summary>
    /// <param name="gameObject">The object to be placed.</param>
    public void PlaceObject(GameObject gameObject)
    {
        if (isPassiveScene)
        {
            /// If the placement pose is valid and the user taps the screen, 
            /// place a new objectToPlace object.
            if (_placementPoseIsValid && _placedObjectCount == 0)
            {
                _placedObjectCount = 1;

                /// Create a new game object at the placement reticle.
                _placedGameObject = Instantiate(gameObject,
                    _placementPose.position, 
                    placementIndicator.transform.rotation);
            }
        }
        else
        {
            if (_placedObjectCount == 0)
            {
                _placedObjectCount = 1;

                activeModelPreview.SetActive(false);

                _placedGameObject = Instantiate(gameObject,
                    new Vector3(Camera.main.transform.position.x + 0.4f, Camera.main.transform.position.y - 1.5f, Camera.main.transform.position.z + 0.25f),
                    Quaternion.Euler(0.0f, Camera.main.transform.eulerAngles.y + 270.0f, 0.0f));
            }
        }
    }

    /// <summary>
    /// DeletePlacedObject is used to delete the placed object and reset buttons.
    /// </summary>
    public void DeletePlacedObject()
    {
        /// Destroy the placed object and decrement _placedObjectCount.
        Destroy(_placedGameObject);
        _placedObjectCount = 0;

        if (!isPassiveScene)
        {
            activeModelPreview.SetActive(true);
        }

        /// Re-enable the rotation and placement buttons.
        for (int i = 0; i < 3; i++)
        {
            _buttonHandler.GetComponent<ButtonHandler>()
                .buttons[i].interactable = true;
        }
    }

    /// <summary>
    /// RotatePlacementIndicator is used to rotate the placement indicator.
    /// </summary>
    /// <param name="direction">The direction to rotate the indicator.</param>
    public void RotatePlacementIndicator(bool direction)
    {
        /// Only rotate the indicator if it is being displayed.
        if (_placementPoseIsValid)
        {
            /// Determine the direction to rotate the indicator in.
            Vector3 rotationDirection = direction ? Vector3.up : -Vector3.up;

            /// Rotate the reticle based on the above direction and rotation speed.
            placementIndicator.transform.Rotate(rotationDirection * _rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// OnRotatePointerDown is used to indicate a rotation button is being pushed.
    /// </summary>
    public void OnRotatePointerDown()
    {
        _rotationPointerDown = true;
    }

    /// <summary>
    /// OnRotatePointerUp is used to indicate a rotation button has been released.
    /// </summary>
    public void OnRotatePointerUp()
    {
        _rotationPointerDown = false;
    }

    /// <summary>
    /// RotateIndicatorRight is used to set rotationDirection to true.
    /// </summary>
    public void RotateIndicatorRight()
    {
        _rotationDirection = true;
    }

    /// <summary>
    /// RotateIndicatorLeft is used to set rotationDirection to false.
    /// </summary>
    public void RotateIndicatorLeft()
    {
        _rotationDirection = false;
    }

    /// <summary>
    /// GetPlacementValidity is for use by other classes to get _placementPoseIsValid.
    /// </summary>
    public bool GetPlacementValidity()
    {
        return _placementPoseIsValid;
    }

    /// <summary>
    /// GetPlacedObjectCount is for use by other classes to get _placedObjectCount.
    /// </summary>
    public int GetPlacedObjectCount()
    {
        return _placedObjectCount;
    }

    /// <summary>
    /// GetPlacedObjectPosition returns the position of _placedGameObject.
    /// </summary>
    public Vector3 GetPlacedObjectPosition()
    {
        return _placedGameObject.transform.position;
    }

    /// <summary>
    /// GetPlacedObjectRotation returns the rotation of _placedGameObject.
    /// </summary>
    public Quaternion GetPlacedObjectRotation()
    {
        return _placedGameObject.transform.rotation;
    }

    /// <summary>
    /// GetPlacedObjectDirection returns the direction of _placedGameObject.
    /// </summary>
    public Vector3 GetPlacedObjectDirection()
    {
        return _placedGameObject.transform.forward;
    }

    public Animator GetPlacedObjectAnimator()
    {
        if (_placedGameObject.GetComponent<Animator>())
        {
            return _placedGameObject.GetComponent<Animator>();
        }
        else
        {
            return null;
        }
    }
}