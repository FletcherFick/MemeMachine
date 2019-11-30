using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue3_5Button : MonoBehaviour
{
    public void ActivateSarahDialogue()
    {
        ActiveSceneHandler.sarahAudioTriggers[4] = true;
    }
}
