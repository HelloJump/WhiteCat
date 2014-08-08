/// <summary>
/// Safe area. using to prevent the enemy enter this area
/// add this to object with Collision Trigger adjust the area size by collision size
/// </summary>
using UnityEngine;
using System.Collections;

public class SafeArea : MonoBehaviour
{

	public string[] UnableTag = {"Enemy"};

	void Start ()
	{
	
	}
	
	void OnTriggerStay (Collider col)
	{
		bool pass = true;
		for (int i=0; i<UnableTag.Length; i++) {
			if (col.gameObject.tag == UnableTag [i]) {
				pass = false;	
			}
		}
		if (!pass) {
		
			if (col.gameObject.GetComponent<AICharacterController> ()) {
				col.gameObject.GetComponent<AICharacterController> ().ObjectTarget = null;
			}
			col.gameObject.transform.position += (-col.gameObject.transform.forward * 0.05f);	
			
		}
	}
}
