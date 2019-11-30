using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue2Button : MonoBehaviour
{
    public void ActivateSarahAudio()
    {
        ActiveSceneHandler.sarahAudioTriggers[3] = true;
    }
}