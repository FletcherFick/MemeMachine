using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSceneHandler : MonoBehaviour
{
    /// <summary>monster references the Monster prefab.</summary>
    /// <value>
    /// monster is used to reference and control different aspects of the
    /// Monster prefab after it is loaded into the scene.
    /// </value>
    public GameObject monster;

    /// <summary>interactionManager references Interaction Manager.</summary>
    /// <value>
    /// interactionManager is used to store a referene to the Interaction 
    /// Manager game object.
    /// </value>
    private GameObject _interactionManager;

    /// <summary>scriptReference references the InteractionHandler script.</summary>
    /// <value>
    /// scriptReference is used to reference the InteractionHandler script
    /// connected to the Interaction Manager game object.
    /// </value>
    private InteractionHandler _scriptReference;

    /// <summary>_storyStarted determines if the story has started.</summary>
    /// <value>
    /// _storyStarted keeps track of if the story is started; it is instantiated
    /// as false and turns true when the Start Story button is tapped.
    /// </value>
    private bool _storyStarted;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        /// Instantiate _storyStarted.
        _storyStarted = false;

        /// Connect interactionManager to the Interaction Manager game object.
        _interactionManager = GameObject.Find("Interaction Handler");

        /// Connect scriptReference to the ARTapToPlace script on interactionManager.
        _scriptReference = _interactionManager.GetComponent<InteractionHandler>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        /// Check if the Start Story button has been tapped.
        if (_storyStarted)
        {
            /// Spawn the monster game object.
            SpawnMonster(monster);
        }
    }

    /// <summary>
    /// StartStory is used by other methods to set _storyStarted to true.
    /// </summary>
    public void StartStory()
    {
        _storyStarted = true;
    }

    /// <summary>
    /// SpawnMonster spawns the monster in front of the placed game object.
    /// </summary>
    /// <param name="monster">The game object representing the monster.</param>
    private void SpawnMonster(GameObject monster)
    {
        /// Prevent the Update loop from running this method more than once.
        _storyStarted = false;

        /// Wait for X seconds.
        StartCoroutine(Wait(10));
        
        /// Obtain placement information about the fireplace.
        Vector3 _fireplacePosition = _scriptReference.GetPlacedObjectPosition();
        Vector3 _fireplaceDirection = _scriptReference.GetPlacedObjectDirection();
        Quaternion _fireplaceRotation = _scriptReference.GetPlacedObjectRotation();

        /// Calculate the monster game object's spawn position.
        Vector3 _spawnPosition = _fireplacePosition + (_fireplaceDirection * 1);

        /// Spawn the monster game object.
        Instantiate(monster, _spawnPosition, _fireplaceRotation);
    }

    /// <summary>
    /// Wait is used to wait for the indicated number of seconds.
    /// </summary>
    /// <param name="seconds">The amount of time to wait.</param>
    private IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
