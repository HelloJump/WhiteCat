using UnityEngine;
using System.Collections;

public class JustTest2Script : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("!!!!!!!!!!!!!!");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A : Down");
        }
	}
}
