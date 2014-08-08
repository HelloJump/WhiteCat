using UnityEngine;
using System.Collections;

public class CharacterSelector : MonoBehaviour
{
	public bool Active = true;
	public GUISkin skin;
	public GameObject CharacterSlected;
	public string Text = "Name";
	public string SceneNameStart = "Setting";
	private bool removeBaseOnStart;
	
	void Start ()
	{
	
	}

	void Update ()
	{
		// !! Spawner is importance. you have to added into you level to spawn a player.
		var spawner = (CharacterSpawner)FindObjectOfType (typeof(CharacterSpawner));
		if (spawner) {
			spawner.Spawn (CharacterSlected);
			if(removeBaseOnStart)
			Destroy(CharacterSlected);
			Destroy (this.gameObject);
		}
	}

	public void StartThisCharacter (bool removeBaseCharacter)
	{
		// Clear sample character or not and start the level
		removeBaseOnStart = removeBaseCharacter;
		DontDestroyOnLoad (this.gameObject);
		Application.LoadLevel (SceneNameStart);
		BlackFade fader = (BlackFade)GameObject.FindObjectOfType (typeof(BlackFade));
		if (fader) {
			fader.Fade (1, 0);
		}
	}
	
	void OnGUI ()
	{
		if(!Active)
			return;
		
		if (skin)
			GUI.skin = skin;
		
		if (Camera.main != null) {
		
			Vector3 screenPos = Camera.main.camera.WorldToScreenPoint (this.gameObject.transform.position);	
			var dir = (Camera.main.camera.transform.position - this.transform.position).normalized;
			var direction = Vector3.Dot (dir, Camera.main.camera.transform.forward);
	    
			if (direction < 0.6f) {
				if (GUI.Button (new Rect (screenPos.x - 75, Screen.height - screenPos.y, 150, 30), Text)) {
					StartThisCharacter (false);
				}
			}
		}
	}
}
