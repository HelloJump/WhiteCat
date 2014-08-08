using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour 
{
    private Vector3 distanceCam2Unit;
    public GameObject followCamera;
    void Awake()
    {
        distanceCam2Unit = transform.localPosition - followCamera.transform.localPosition;
    }

    void FixedUpdate()
    {
        followCamera.transform.localPosition = transform.localPosition - distanceCam2Unit;
    }
}
