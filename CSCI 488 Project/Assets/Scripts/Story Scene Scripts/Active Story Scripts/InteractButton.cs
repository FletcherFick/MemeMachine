using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    public void ActivateRadioInteraction()
    {
        ActiveSceneHandler.interactedWithRadio = true;
    }

    public void ActivateLockInteraction()
    {
        ActiveSceneHandler.interactedWithDoorLock = true;
    }

    public void ActivateWheelInteraction()
    {
        ActiveSceneHandler.interactedWithSteeringWheel = true;
    }

    public void ActivateDoorInteraction()
    {
        ActiveSceneHandler.interactedWithDoor = true;
    }

    public void ActivateHookInteraction()
    {
        ActiveSceneHandler.interactedWithHook = true;
    }
}