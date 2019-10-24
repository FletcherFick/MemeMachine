using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ButtonHandler is used to manage the interactivity of the UI buttons.
/// </summary>
public class ButtonHandler : MonoBehaviour
{
    /// <summary>_buttons is used to store references to all UI buttons.</summary>
    /// <value>
    /// _buttons is a list that stores references to all UI buttons related
    /// to the open scene and is used to interact with them.
    /// </value>
    public List<Button> buttons;

    /// <summary>_interactionHandler is used to reference InteractionHandler.</summary>
    /// <value>
    /// _interactionHandler is used to reference the InteractionHandler script
    /// and call its public methods.
    /// </value>
    private InteractionHandler _interactionHandler;

    /// <summary>_hasDisabledButtons is used to determine if the buttons are disabled.</summary>
    /// <value>
    /// _hasDsiabledButtons is used to keep track of if the buttons have been
    /// put into their interactable or uninteractable state.
    /// </value>
    private bool _hasDisabledButtons;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        /// Link the InteractionHandler script to _interactionHandler.
        _interactionHandler = GameObject.Find("Interaction Handler")
            .GetComponent<InteractionHandler>();

        /// Assume no buttons have been disabled.
        _hasDisabledButtons = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        /// Check if there is a placed object in the scene.
        if (_interactionHandler.GetPlacedObjectCount() == 0)
        {
            /// Disable the undo and start scene buttons.
            for (int i = 3; i < 5; i++)
            {
                buttons[i].interactable = false;
            }

            /// Check if the placement pose of the placement indicator is valid.
            if (!_interactionHandler.GetPlacementValidity())
            {
                /// The pose is not valid; adjust all UI placement buttons.
                for (int i = 0; i < 3; i++)
                {
                    buttons[i].interactable = false;
                }

                /// Update _hasDisabledButtons.
                _hasDisabledButtons = true;
            }
            else if (_hasDisabledButtons)
            {
                /// The pose is valid; enable all UI placement buttons.
                for (int i = 0; i < 3; i++)
                {
                    buttons[i].interactable = true;
                }

                /// Update _hasDisabledButtons.
                _hasDisabledButtons = false;
            }
        }
        else
        {
            /// Adjust the button interactability.
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i < 3)
                {
                    /// Disable the rotation and placement buttons.
                    buttons[i].interactable = false;
                }
                else
                {
                    /// Enable the undo and start scene buttons.
                    buttons[i].interactable = true;
                }
            }
        }
    }

    /// <summary>
    /// ClearObjectPlacementButtons is used to set all buttons involved in the
    /// object placement process to inactive.
    /// </summary>
    public void ClearObjectPlacementButtons()
    {
        /// Set the first five buttons in _buttons to inactive.
        for (int i = 0; i < 5; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
}