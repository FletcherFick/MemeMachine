using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModelPreview : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 prefabPosition = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z);
        Quaternion prefabRotation = Quaternion.Euler(0.0f, Camera.main.transform.eulerAngles.y + 270.0f, 0.0f);

        transform.position = prefabPosition;
        transform.rotation = prefabRotation;
    }
}