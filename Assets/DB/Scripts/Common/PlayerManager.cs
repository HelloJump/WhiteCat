using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInstance))]
[RequireComponent(typeof(PlayerCharacterUI))]
[RequireComponent(typeof(PlayerQuestManager))]
[RequireComponent(typeof(PlayerSave))]

public class PlayerManager : MonoBehaviour
{
	[HideInInspector]
	public GameObject Player;
	private string doorEnterID;
	
	void Start ()
	{
		this.GetComponent<PlayerCharacterUI> ().Active = true;
	}
	
	void Awake ()
	{
		Player = this.gameObject;
		DontDestroyOnLoad (this.gameObject);
	}

	public void enterDoor (Teleporter doorIn)
	{
		
		if (doorIn) {
			doorEnterID = doorIn.DoorID;
			Debug.Log ("Enter.." + doorIn.name);
		}
	}

	public void Teleport (Teleporter doorOut)
	{
		Teleporter[] doors = (Teleporter[])GameObject.FindObjectsOfType (typeof(Teleporter));
		for (int i=0; i<doors.Length; i++) {
			if (doorEnterID == doorOut.DoorID) {
				Player.transform.position = doorOut.transform.position + doorOut.SpawnOffset;
				Debug.Log ("Teleported " + doorOut.name);
				break;
			}
		}
	}
	
	public void Spawn (GameObject CharacterSlected)
	{
		if (CharacterSlected) {
			GameObject player = (GameObject)GameObject.Instantiate (CharacterSlected, this.transform.position, Quaternion.identity);
			Player = player;
		}
	}

}
