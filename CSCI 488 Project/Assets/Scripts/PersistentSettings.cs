using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSettings : MonoBehaviour
{
    public static bool _muteStatus = false;
    public static bool _subtitleStatus = false;

    public void SetMuteStatus(bool status)
    {
        _muteStatus = status;
    }

    public void SetSubtitleStatus(bool status)
    {
        _subtitleStatus = status;
    }

    public bool GetMuteStatus()
    {
        return _muteStatus;
    }

    public bool GetSubtitleStatus()
    {
        return _subtitleStatus;
    }
}
