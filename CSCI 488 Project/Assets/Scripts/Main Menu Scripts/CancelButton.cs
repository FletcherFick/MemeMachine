using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public GameObject confirmationWindow;
    public GameObject screenShade;

    public void Cancel()
    {
        confirmationWindow.SetActive(false);
        screenShade.SetActive(false);
    }
}
