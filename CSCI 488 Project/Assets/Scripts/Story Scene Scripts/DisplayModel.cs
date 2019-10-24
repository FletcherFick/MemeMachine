using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Display model is used to display the object that is going to be placed
/// when the scene first loads.
/// </summary>
public class DisplayModel : MonoBehaviour
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        /// Obtain the object being used in the InteractionHandler script.
        GameObject modelToDisplay = GameObject.Find("Interaction Handler")
            .GetComponent<InteractionHandler>().objectToPlace;

        /// Place the object at the Object Display game object.
        GameObject displayedModel = Instantiate(modelToDisplay, 
            transform.position, Quaternion.Euler(5, 160, 0));
        
        /// Modify the object's settings to display appropriately.
        displayedModel.transform.localScale = new Vector3(500, 500, 500);
        displayedModel.transform.parent = GameObject.Find("Object Display").transform;
        displayedModel.layer = 8;
        displayedModel.transform.GetChild(0).gameObject.layer = 8;

        /// Ensure all parts of the object are set to render.
        foreach (Transform child in displayedModel.transform.GetChild(0))
        {
            child.gameObject.layer = 8;

            foreach (Transform subChild in child)
            {
                subChild.gameObject.layer = 8;
            }
        }
    }
}