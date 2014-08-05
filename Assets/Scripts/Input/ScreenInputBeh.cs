using UnityEngine;
using System.Collections;

public class ScreenInputBeh : MonoBehaviour 
{
    public GameObject InputMessageReceiver;
    void OnClick()
    {
        if (InputMessageReceiver == null)
        {
            Debug.LogWarning("InputMessageReceiver is null");
        }
        InputMessageReceiver.SendMessage("AttackListener");
    }
}
