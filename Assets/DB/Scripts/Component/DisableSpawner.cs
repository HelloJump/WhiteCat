using UnityEngine;
using System.Collections;

public class DisableSpawner : MonoBehaviour
{

	void ActiveEnemySpawner (bool eneble)
	{
		EnemySpawner[] spawners = (EnemySpawner[])GameObject.FindObjectsOfType (typeof(EnemySpawner));
		for (int i=0; i<spawners.Length; i++) {
			spawners [i].gameObject.SetActive (eneble);	
		}
	}

	void Start ()
	{
		ActiveEnemySpawner (false);
	}
	
}
