using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue3Button : MonoBehaviour
{
    public void ActivatePlayerDialogue()
    {
        ActiveSceneHandler.playerDialogueTriggers[3] = true;
    }
}
