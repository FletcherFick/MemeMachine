using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ActiveSceneHandler : MonoBehaviour
{
    public GameObject activeCrosshair;
    public GameObject inactiveCrosshair;

    public List<Button> interactButtons;

    public List<GameObject> subtitles;
    public List<Button> dialogue;
    public List<GameObject> instructions;

    public static AudioClip radioReport;

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

    private bool _hasStartedRadio;
    private AudioSource _audioSource;

    private AudioClip[] _sarahDialogue;

    public GameObject passiveSceneHandler;
    private GameObject[] hitboxes = new GameObject[8];

    private AudioClip scratching;
    private AudioClip carEngine;

    public GameObject transitionScreen;
    
    private GameObject _persistentSettings;
    private PersistentSettings _settingsScript;
    public GameObject muteButton;

    // Awake is called before the first frame update
    void Awake()
    {
        _storyStarted = false;
        _hasStartedRadio = false;
        activeCrosshair.SetActive(false);
        inactiveCrosshair.SetActive(false);
        transitionScreen.SetActive(false);

        radioReport = Resources.Load<AudioClip>("radio_report");

        _sarahDialogue = new AudioClip[9];
        _sarahDialogue[0] = Resources.Load<AudioClip>("sarah_1");
        _sarahDialogue[1] = Resources.Load<AudioClip>("sarah_2");
        _sarahDialogue[2] = Resources.Load<AudioClip>("sarah_3");
        _sarahDialogue[3] = Resources.Load<AudioClip>("sarah_4");
        _sarahDialogue[4] = Resources.Load<AudioClip>("sarah_5");
        _sarahDialogue[5] = Resources.Load<AudioClip>("sarah_6");
        _sarahDialogue[6] = Resources.Load<AudioClip>("sarah_7");
        _sarahDialogue[7] = Resources.Load<AudioClip>("sarah_8");
        _sarahDialogue[8] = Resources.Load<AudioClip>("sarah_9");

        scratching = Resources.Load<AudioClip>("scratching");
        carEngine = Resources.Load<AudioClip>("car_engine");

        /// Connect _arRaycastManager to the AR Session Origin's raycast manager.
        _arRaycastManager = FindObjectOfType<ARSessionOrigin>().
            GetComponent<ARRaycastManager>();

        /// Connect _interactionHandler to the Interaction Manager game object.
        _interactionHandler = GameObject.Find("Interaction Handler");

        /// Connect _interactionScript to the ARTapToPlace script on interactionManager.
        _interactionScript = _interactionHandler.GetComponent<InteractionHandler>();

        _audioSource = GetComponent<AudioSource>();

        _persistentSettings = GameObject.Find("Persistent Settings");
        _settingsScript = _persistentSettings.GetComponent<PersistentSettings>();
    }

    public static bool[] sarahAudioTriggers = new bool[9];
    public static bool[] playerDialogueTriggers = new bool[7];
    public static bool activateLockHitbox = false;
    public static bool drivingTrigger = false;
    private bool scratchTrigger = false;
    private bool showTransitionScreen = false;
    public static bool activateDoorHitbox = false;
    private bool finalStoryAct = false;

    public static bool interactedWithRadio = false;
    public static bool interactedWithDoorLock = false;
    public static bool interactedWithSteeringWheel = false;
    public static bool interactedWithDoor = false;
    public static bool interactedWithHook = false;

    private bool sceneInitiated = false;
    public GameObject endScreen;
    public GameObject exitButton;

    // Update is called once per frame
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

        if (_storyStarted)
        {
            if (!sceneInitiated)
            {
                sceneInitiated = true;
                //interactButton.gameObject.SetActive(true);
                interactButtons[0].gameObject.SetActive(true);
                passiveSceneHandler.SetActive(false);

                hitboxes[0] = GameObject.Find("RadioHitbox");
                hitboxes[1] = GameObject.Find("DoorLockHitbox");
                hitboxes[2] = GameObject.Find("DoorHitbox1");
                hitboxes[3] = GameObject.Find("DoorHitbox2");
                hitboxes[4] = GameObject.Find("DoorHitbox3");
                hitboxes[5] = GameObject.Find("DoorHitbox4");
                hitboxes[6] = GameObject.Find("SteeringWheelHitbox");
                hitboxes[7] = GameObject.Find("Hook");

                hitboxes[1].SetActive(false);
                hitboxes[2].SetActive(false);
                hitboxes[3].SetActive(false);
                hitboxes[4].SetActive(false);
                hitboxes[5].SetActive(false);
                hitboxes[6].SetActive(false);
                hitboxes[7].SetActive(false);

                instructions[0].SetActive(true);
            }

            CursorCheck();

            if (interactedWithRadio && !_hasStartedRadio)
            {
                _hasStartedRadio = true;
                instructions[0].SetActive(false);
                _audioSource.PlayOneShot(radioReport);
                hitboxes[0].SetActive(false);
            }

            if (_hasStartedRadio && !playerDialogueTriggers[0])
            {
                if (!_audioSource.isPlaying)
                {
                    sarahAudioTriggers[0] = true;
                }

                if (sarahAudioTriggers[0])
                {
                    _audioSource.PlayOneShot(_sarahDialogue[0]);
                    playerDialogueTriggers[0] = true;
                }
            }

            if (playerDialogueTriggers[0] && !activateLockHitbox)
            {
                if (!_audioSource.isPlaying)
                {
                    dialogue[0].interactable = true;
                    dialogue[0].gameObject.SetActive(true);
                }
            }

            if (activateLockHitbox && !interactedWithDoorLock)
            {
                interactButtons[0].gameObject.SetActive(false);
                interactButtons[1].gameObject.SetActive(true);
                instructions[1].SetActive(true);
                hitboxes[1].SetActive(true);
                dialogue[0].interactable = false;
                dialogue[0].gameObject.SetActive(false);
            }

            if (interactedWithDoorLock && !sarahAudioTriggers[1])
            {
                hitboxes[1].SetActive(false);
                instructions[1].SetActive(false);
                sarahAudioTriggers[1] = true;
            }

            if (sarahAudioTriggers[1] && !sarahAudioTriggers[2])
            {
                _audioSource.PlayOneShot(_sarahDialogue[1]);
                sarahAudioTriggers[2] = true;
            }

            if (sarahAudioTriggers[2] && !playerDialogueTriggers[1])
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.PlayOneShot(_sarahDialogue[2]);
                    playerDialogueTriggers[1] = true;
                }
            }

            if (playerDialogueTriggers[1] && !sarahAudioTriggers[3])
            {
                if (!_audioSource.isPlaying)
                {
                    dialogue[1].interactable = true;
                    dialogue[1].gameObject.SetActive(true);
                }
            }

            if (sarahAudioTriggers[3] && !playerDialogueTriggers[2])
            {
                dialogue[1].interactable = false;
                dialogue[1].gameObject.SetActive(false);
                _audioSource.PlayOneShot(_sarahDialogue[3]);
                playerDialogueTriggers[2] = true;
            }

            if (playerDialogueTriggers[2] && !playerDialogueTriggers[3])
            {
                if (!_audioSource.isPlaying)
                {
                    dialogue[2].interactable = true;
                    dialogue[2].gameObject.SetActive(true);
                }
            }

            if (playerDialogueTriggers[3] && !sarahAudioTriggers[4])
            {
                dialogue[2].interactable = false;
                dialogue[2].gameObject.SetActive(false);
                dialogue[3].interactable = true;
                dialogue[3].gameObject.SetActive(true);
            }

            if (sarahAudioTriggers[4] && !sarahAudioTriggers[5])
            {
                dialogue[3].interactable = false;
                dialogue[3].gameObject.SetActive(false);
                _audioSource.PlayOneShot(_sarahDialogue[4]);
                sarahAudioTriggers[5] = true;
            }

            if (sarahAudioTriggers[5] && !playerDialogueTriggers[4])
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.PlayOneShot(_sarahDialogue[5]);
                    playerDialogueTriggers[4] = true;
                }
            }

            if (playerDialogueTriggers[4] && !drivingTrigger)
            {
                if (!_audioSource.isPlaying)
                {
                    interactButtons[1].gameObject.SetActive(false);
                    interactButtons[2].gameObject.SetActive(true);
                    dialogue[4].interactable = true;
                    dialogue[4].gameObject.SetActive(true);
                }
            }

            if (drivingTrigger && !scratchTrigger)
            {
                dialogue[4].interactable = false;
                dialogue[4].gameObject.SetActive(false);
                instructions[2].SetActive(true);
                hitboxes[6].SetActive(true);

                if (interactedWithSteeringWheel)
                {
                    showTransitionScreen = true;
                    activeCrosshair.SetActive(false);
                    inactiveCrosshair.SetActive(false);
                    //interactButton.gameObject.SetActive(false);
                    instructions[2].SetActive(false);
                    hitboxes[6].SetActive(false);
                    _audioSource.PlayOneShot(carEngine);
                    scratchTrigger = true;
                }
            }

            if (scratchTrigger && !sarahAudioTriggers[6])
            {
                StartCoroutine(PlayScratchingAudio());
                sarahAudioTriggers[6] = true;
                sarahAudioTriggers[7] = true;
            }

            if (sarahAudioTriggers[6] && sarahAudioTriggers[7] && !playerDialogueTriggers[5])
            {
                playerDialogueTriggers[5] = true;
                StartCoroutine(PlayerSarahAudio6_7());
            }

            if (showTransitionScreen && playerDialogueTriggers[5])
            {
                activeCrosshair.SetActive(false);
                inactiveCrosshair.SetActive(false);
                foreach (Button button in interactButtons)
                {
                    button.gameObject.SetActive(false);
                }
                transitionScreen.SetActive(true);
            }

            if (sarahAudioTriggers[8] && !playerDialogueTriggers[6])
            {
                sarahAudioTriggers[8] = false;
                StartCoroutine(DisableTransitionScreen());
            }

            if (playerDialogueTriggers[6] && !activateDoorHitbox)
            {
                if (!_audioSource.isPlaying)
                {
                    dialogue[6].interactable = true;
                    dialogue[6].gameObject.SetActive(true);
                }
            }

            if (activateDoorHitbox && !finalStoryAct)
            {
                interactButtons[2].gameObject.SetActive(false);
                interactButtons[3].gameObject.SetActive(true);
                instructions[3].SetActive(true);
                dialogue[6].interactable = false;
                dialogue[6].gameObject.SetActive(false);
                hitboxes[2].SetActive(true);
                hitboxes[3].SetActive(true);
                hitboxes[4].SetActive(true);
                hitboxes[5].SetActive(true);
            }

            if (interactedWithDoor && activateDoorHitbox && !finalStoryAct)
            {
                hitboxes[2].SetActive(false);
                hitboxes[3].SetActive(false);
                hitboxes[4].SetActive(false);
                hitboxes[5].SetActive(false);
                instructions[3].SetActive(false);
                _interactionScript.GetPlacedObjectAnimator().SetBool("openDoor", true);
                _interactionScript.GetPlacedObjectAnimator().SetBool("closeDoor", false);
                finalStoryAct = true;
            }

            if (finalStoryAct && !interactedWithHook)
            {
                instructions[4].SetActive(true);
                hitboxes[7].SetActive(true);
                interactButtons[3].gameObject.SetActive(false);
                interactButtons[4].gameObject.SetActive(true);
            }

            if (interactedWithHook)
            {
                activeCrosshair.SetActive(false);
                inactiveCrosshair.SetActive(false);
                interactButtons[4].gameObject.SetActive(false);
                endScreen.SetActive(true);
                muteButton.SetActive(false);
                exitButton.SetActive(false);
                instructions[4].SetActive(false);
                hitboxes[7].SetActive(false);

                ResetTriggerValues();
            }
        }
    }

    private void CursorCheck()
    {
        // Determine where the center of the screen is.
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, Camera.main.nearClipPlane));
            
        RaycastHit hit;

        if (Physics.Raycast(screenCenter, Camera.main.transform.forward, out hit, 10000.0f))
        {
            if (hit.transform.gameObject.tag == "CarLock")
            {
                if (!transitionScreen.activeSelf)
                {
                    //interactButton.interactable = true;
                    interactButtons[1].interactable = true;
                    activeCrosshair.SetActive(true);
                    inactiveCrosshair.SetActive(false);
                }
            }

            if (hit.transform.gameObject.tag == "ExteriorHandle")
            {
                if (!transitionScreen.activeSelf)
                {
                    //interactButton.interactable = true;
                    interactButtons[3].interactable = true;
                    activeCrosshair.SetActive(true);
                    inactiveCrosshair.SetActive(false);
                }
            }
            
            if (hit.transform.gameObject.tag == "Radio")
            {
                if (!transitionScreen.activeSelf)
                {
                    //interactButton.interactable = true;
                    interactButtons[0].interactable = true;
                    activeCrosshair.SetActive(true);
                    inactiveCrosshair.SetActive(false);
                }
            }
            
            if (hit.transform.gameObject.tag == "SteeringWheel")
            {
                if (!transitionScreen.activeSelf)
                {
                    //interactButton.interactable = true;
                    interactButtons[2].interactable = true;
                    activeCrosshair.SetActive(true);
                    inactiveCrosshair.SetActive(false);
                }
            }

            if (hit.transform.gameObject.tag == "Hook")
            {
                if (!transitionScreen.activeSelf)
                {
                    //interactButton.interactable = true;
                    interactButtons[4].interactable = true;
                    activeCrosshair.SetActive(true);
                    inactiveCrosshair.SetActive(false);
                }
            }
        }
        else
        {
            //interactButton.interactable = false;
            foreach (Button button in interactButtons)
            {
                button.interactable = false;
            }
            activeCrosshair.SetActive(false);
            inactiveCrosshair.SetActive(true);
        }
    }

    private IEnumerator PlayScratchingAudio()
    {
        yield return new WaitForSeconds(1.0f);
        _audioSource.PlayOneShot(scratching);
    }

    private IEnumerator PlayerSarahAudio6_7()
    {
        yield return new WaitForSeconds(4.25f);
        _audioSource.PlayOneShot(_sarahDialogue[6]);
        yield return new WaitForSeconds(1.0f);
        _audioSource.PlayOneShot(_sarahDialogue[7]);
        yield return new WaitForSeconds(3.5f);
        dialogue[5].interactable = true;
        dialogue[5].gameObject.SetActive(true);
    }

    private IEnumerator DisableTransitionScreen()
    {
        yield return new WaitForSeconds(2.0f);

        showTransitionScreen = false;
        transitionScreen.SetActive(false);
        activeCrosshair.SetActive(false);
        inactiveCrosshair.SetActive(true);
        interactButtons[2].gameObject.SetActive(true);
        //interactButton.gameObject.SetActive(true);
        dialogue[5].interactable = false;
        dialogue[5].gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        _audioSource.PlayOneShot(_sarahDialogue[8]);
        playerDialogueTriggers[6] = true;
    }

    public void ResetTriggerValues()
    {
        _storyStarted = false;
        _hasStartedRadio = false;
        activateLockHitbox = false;
        drivingTrigger = false;
        scratchTrigger = false;
        showTransitionScreen = false;
        activateDoorHitbox = false;
        finalStoryAct = false;
        interactedWithRadio = false;
        interactedWithDoorLock = false;
        interactedWithSteeringWheel = false;
        interactedWithDoor = false;
        interactedWithHook = false;

        for (int i = 0; i < sarahAudioTriggers.Length; i++)
        {
            sarahAudioTriggers[i] = false;
        }

        for (int i = 0; i < playerDialogueTriggers.Length; i++)
        {
            playerDialogueTriggers[i] = false;
        }
    }

    /// <summary>
    /// StartStory is used by other classes to set _storyStarted to true.
    /// </summary>
    public void StartStory()
    {
        _storyStarted = true;
    }

    public void DisableTransitionDialogue()
    {
        dialogue[5].interactable = false;
        dialogue[5].gameObject.SetActive(false);
    }
}