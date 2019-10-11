using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

/// <summary>
/// ARTapToPlaceObject is used to allow the player to see the placement
/// reticle and lets them place an object at its position.
///</summary>
public class ARTapToPlaceObject : MonoBehaviour
{
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
    private bool _placementPoseIsValid = false;

    /// <summary>_rotationSpeed is how fast the placement reticle rotates.</summary>
    /// <value>
    /// _rotationSpeed is used to set how fast the placement indicator rotates;
    /// the higher the value, the faster the rotation.
    /// </value>
    private float _rotationSpeed = 100.0f;

    /// <summary>rotationPointerDown determines if a rotate button is down.</summary>
    /// <value>
    /// rotationPointerDown is used to determine if either the left or right
    /// object rotation buttons are being pressed.
    /// </value>
    public bool rotationPointerDown;

    /// <summary>rotationDirection determines how to rotate the reticle.</summary>
    /// <value>
    /// rotationDirection is used to determine which direction to rotate
    /// the placement reticle; true is right, false is left.
    /// </value>
    public bool rotationDirection;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        /// Connect _arRaycastManager to the AR Session Origin's raycast manager.
        _arRaycastManager = FindObjectOfType<ARSessionOrigin>().
            GetComponent<ARRaycastManager>();

        /// Set the initial rotation for the placement reticle.
        placementIndicator.transform.rotation = Quaternion.Euler(Vector3.forward);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        /// Update the pose of the placement reticle.
        UpdatePlacementPose();

        /// Update the placement reticle game object's position.
        UpdatePlacementIndicator();

        /// If a rotation button is being pushed, rotate the placement reticle.
        if (rotationPointerDown)
        {
            RotatePlacementIndicator(rotationDirection);
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
        if (_placementPoseIsValid)
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
        /// If the placement pose is valid and the user taps the screen, 
        /// place a new objectToPlace object.
        if (_placementPoseIsValid)
        {
            /// Create a new game object at the placement reticle.
            Instantiate(gameObject,
                _placementPose.position, 
                placementIndicator.transform.rotation);
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
        rotationPointerDown = true;
    }

    /// <summary>
    /// OnRotatePointerUp is used to indicate a rotation button has been released.
    /// </summary>
    public void OnRotatePointerUp()
    {
        rotationPointerDown = false;
    }

    /// <summary>
    /// RotateIndicatorRight is used to set rotationDirection to true.
    /// </summary>
    public void RotateIndicatorRight()
    {
        rotationDirection = true;
    }

    /// <summary>
    /// RotateIndicatorLeft is used to set rotationDirection to false.
    /// </summary>
    public void RotateIndicatorLeft()
    {
        rotationDirection = false;
    }
}