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
        /// Obtain the object being used in the ARTapToPlaceObject script.
        GameObject modelToDisplay = GameObject.Find("Interaction Handler")
            .GetComponent<ARTapToPlaceObject>().objectToPlace;

        /// Place the object at the Object Display game object.
        GameObject displayedModel = Instantiate(modelToDisplay, 
            transform.position, Quaternion.Euler(-10, -30, 0));
        
        /// Modify the object's settings to display appropriately.
        displayedModel.transform.localScale = new Vector3(5000, 5000, 5000);
        displayedModel.transform.parent = GameObject.Find("Object Display").transform;
        displayedModel.layer = 8;
        displayedModel.transform.GetChild(0).gameObject.layer = 8;
    }
}