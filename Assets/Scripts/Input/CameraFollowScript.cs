using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour 
{
    private Vector3 cameraPos = new Vector3(0f,3f,-8f);
    private Vector3 distanceCam2Unit;
    public GameObject followCamera;

    private bool hasCaculate = false;
    void Update()
    {
        if (!hasCaculate && followCamera != null)
        {
            followCamera.transform.localPosition = cameraPos;
            distanceCam2Unit = transform.localPosition - followCamera.transform.localPosition;
            hasCaculate = true;
        }
    }

    void FixedUpdate()
    {
        if (followCamera != null)
        {
            followCamera.transform.localPosition = transform.localPosition - distanceCam2Unit;
        }
    }
}
