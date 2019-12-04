using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PassiveSceneHandler is used to handle events that happen after the
/// user taps the Start Story button.
/// </summary>
public class PassiveSceneHandler : MonoBehaviour
{
    /// <summary>monster references the Monster prefab.</summary>
    /// <value>
    /// monster is used to reference and control different aspects of the
    /// Monster prefab after it is loaded into the scene.
    /// </value>
    public GameObject monster;

    /// <summary>_spawnedMonster is a reference to the spawned monster.</summary>
    /// <value>
    /// _spawnedMonster is a reference to the monster game object prefab
    /// that is spawned in front of the fireplace.
    /// </value>
    private GameObject _spawnedMonster;

    /// <summary>_player is a reference to the AR Camera.</summary>
    /// <value>
    /// _player is a reference to the AR Camera game object and is used to
    /// determine the player's position in the scene.
    /// </value>
    private GameObject _player;

    /// <summary>_audioSource is used to play audio.</summary>
    /// <value>
    /// _audioSource is connected to the Passive Scene Handler's 
    /// audio source component and is used to play audio.
    /// </value>
    private AudioSource _audioSource;

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

    /// <summary>_buttonHandler is a reference to ButtonHandler.</summary>
    /// <value>
    /// _buttonHandler is used to reference the ButtonHandler script on the 
    /// Button Handler game object.
    /// </value>
    private GameObject _buttonHandler;

    /// <summary>_endScreen is reference to the End Screen game object.</summary>
    /// <value>
    /// _endScreen is used to reference the End Screen game object located
    /// under the UI Components tab.
    /// </value>
    [SerializeField]
    private GameObject _endScreen;

    /// <summary>_storyStarted determines if the story has started.</summary>
    /// <value>
    /// _storyStarted keeps track of if the story is started; it is instantiated
    /// as false and turns true when the Start Story button is tapped.
    /// </value>
    private bool _storyStarted;

    /// <summary>_showDialogue determines if the user dialogue option is visible.</summary>
    /// <value>
    /// _showDialogue is used to make the button containing the user's dialogue
    /// option visible and interactable.
    /// </value>
    private bool _showDialogue;

    /// <summary>_dialogueCompleted determines if the user activated their dialogue.</summary>
    /// <value>
    /// _dialogueCompleted is set to true once the user taps their dialogue button
    /// and is used to determine if the button should be set to inactive.
    /// </value>
    private bool _dialogueCompleted;

    /// <summary>_chasePlayer is used to determine if the chase scene begins.</summary>
    /// <value>
    /// _chasePlayer is used to determine if the _spawnedMonster game object
    /// should start moving towards the user's position.
    /// </value>
    private bool _chasePlayer;

    public GameObject subtitles;

    private GameObject _persistentSettings;
    private PersistentSettings _settingsScript;

    public GameObject hamburgerButton;
    public GameObject muteButton;
    public GameObject subtitleButton;
    public GameObject exitButton;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        /// Instantiate event-related variables.
        _storyStarted = false;
        _showDialogue = false;
        _dialogueCompleted = false;
        _chasePlayer = false;

        /// Connect _player to the AR Camera game object.
        _player = GameObject.Find("AR Camera");

        /// Connect _audioSource to Passive Scene Handler's audio source component.
        _audioSource = GetComponent<AudioSource>();

        /// Connect _buttonHandler to the ButtonHandler script.
        _buttonHandler = GameObject.Find("Button Handler");

        /// Connect _interactionHandler to the Interaction Manager game object.
        _interactionHandler = GameObject.Find("Interaction Handler");

        /// Connect _interactionScript to the ARTapToPlace script on interactionManager.
        _interactionScript = _interactionHandler.GetComponent<InteractionHandler>();

        _persistentSettings = GameObject.Find("Persistent Settings");
        _settingsScript = _persistentSettings.GetComponent<PersistentSettings>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_settingsScript.GetMuteStatus())
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

        /// Check if the Start Story button has been tapped.
        if (_storyStarted)
        {
            /// Spawn the monster game object.
            SpawnMonster(monster);
        }

        /// Determine if the user's dialogue option should appear.
        if (_showDialogue)
        {
            /// Set the user's dialogue option to active and interactable.
            _buttonHandler.GetComponent<ButtonHandler>().buttons[5].gameObject.SetActive(true);
            _buttonHandler.GetComponent<ButtonHandler>().buttons[5].interactable = true;
        }
        
        /// Determine if the user tapped their dialogue option.
        if (_dialogueCompleted)
        {
            /// Set the user's dialogue option to inactive and uninteractable.
            _dialogueCompleted = false;
            _showDialogue = false;
            _buttonHandler.GetComponent<ButtonHandler>().buttons[5].gameObject.SetActive(false);

            /// Wait X seconds.
            StartCoroutine(Wait(1));

            /// Play the spawned monster's audio response.
            StartCoroutine(PlayAudioSequence());
        }

        /// Determine if the spawned monster should chase the player.
        if (_chasePlayer)
        {
            /// Make the monster chase the player.
            ChasePlayer();
        }
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
        StartCoroutine(Wait(5));
        
        /// Obtain placement information about the fireplace.
        Vector3 _fireplacePosition = _interactionScript.GetPlacedObjectPosition();
        Vector3 _fireplaceDirection = _interactionScript.GetPlacedObjectDirection();
        Quaternion _fireplaceRotation = _interactionScript.GetPlacedObjectRotation();

        /// Calculate the monster game object's spawn position.
        Vector3 _spawnPosition = _fireplacePosition + (_fireplaceDirection * 1);

        /// Spawn the monster game object.
        _spawnedMonster = Instantiate(monster, _spawnPosition, _fireplaceRotation);

        /// Allow the user's dialogue option to be displayed.
        _showDialogue = true;
    }

    /// <summary>
    /// ChasePlayer makes the monster chase the player and determines if it
    /// has reached their position.
    /// </summary>
    private void ChasePlayer()
    {
        /// Calculate how the monster should move.
        _spawnedMonster.transform.Rotate((Vector3.right * 50) * Time.deltaTime);
        Vector3 _playerPosition = new Vector3(_player.transform.position.x, 
            _spawnedMonster.transform.position.y, _player.transform.position.z);
            
        /// Move the monster.
        _spawnedMonster.transform.position = 
            Vector3.MoveTowards(_spawnedMonster.transform.position, 
            _playerPosition, 5 * Time.deltaTime);

        /// Check if the monster has reached the player.
        if (_spawnedMonster.transform.position.x == _player.transform.position.x &&
            _spawnedMonster.transform.position.z == _player.transform.position.z)
        {
            /// The monster has reached the player; display the end screen.
            _endScreen.gameObject.SetActive(true);
            hamburgerButton.SetActive(false);
            muteButton.SetActive(false);
            subtitleButton.SetActive(false);
            exitButton.SetActive(false);
        }
    }

    /// <summary>
    /// Wait is used to wait for the indicated number of seconds.
    /// </summary>
    /// <param name="seconds">The amount of time to wait.</param>
    private IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    /// <summary>
    /// PlayAudioSequence is used to play the monster's audio response.
    /// </summary>
    private IEnumerator PlayAudioSequence()
    {
        if (_settingsScript.GetSubtitleStatus() && _settingsScript.GetSubtitleStatus())
        {
            subtitles.SetActive(true);
        }

        /// Play the monster's audio response and wait for it to finish.
        _audioSource.Play();
        while (_audioSource.isPlaying)
        {
            yield return null;
        }

        if (subtitles.activeSelf && _settingsScript.GetSubtitleStatus())
        {
            subtitles.SetActive(false);
        }

        /// Allow the monster to chase the player.
        _chasePlayer = true;
    }

    /// <summary>
    /// StartStory is used by other classes to set _storyStarted to true.
    /// </summary>
    public void StartStory()
    {
        _storyStarted = true;
    }

    /// <summary>
    /// ActivateDialogue is used by other classes to set _dialogueCompleted to true.
    /// </summary>
    public void ActivateDialogue()
    {
        _dialogueCompleted = true;
    }
}
