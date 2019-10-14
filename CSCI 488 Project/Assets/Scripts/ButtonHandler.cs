using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ButtonHandler is used to manage the interactivity of the UI buttons.
/// </summary>
public class ButtonHandler : MonoBehaviour
{
    /// <summary>_arTapToPlaceObject is used to reference ARTapToPlaceObject.</summary>
    /// <value>
    /// _arTapToPlaceObject is used to reference the ARTapToPlaceObject script
    /// and call its public methods.
    /// </value>
    private ARTapToPlaceObject _arTapToPlaceObject;

    /// <summary>_buttons is used to store references to all UI buttons.</summary>
    /// <value>
    /// _buttons is a list that stores references to all UI buttons related
    /// to the open scene and is used to interact with them.
    /// </value>
    [SerializeField]
    private List<Button> _buttons;

    private bool _hasDisabledButtons;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        /// Link the ARTapToPlaceObject script to _arTapToPlaceObject.
        _arTapToPlaceObject = GameObject.Find("Interaction Handler")
            .GetComponent<ARTapToPlaceObject>();

        _hasDisabledButtons = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        /// Check if the placement pose of the placement indicator is valid.
        if (!_arTapToPlaceObject.GetPlacementValidity())
        {
            /// The pose is not valid; disable all UI buttons.
            foreach (Button button in _buttons)
            {
                button.interactable = false;
            }

            /// Update _hasDisabledButtons.
            _hasDisabledButtons = true;
        }
        else if (_hasDisabledButtons)
        {
            /// The pose is valid; enable all UI buttons.
            foreach (Button button in _buttons)
            {
                button.interactable = true;
            }

            /// Update _hasDisabledButtons.
            _hasDisabledButtons = false;
        }
    }
}