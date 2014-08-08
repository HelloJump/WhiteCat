/// <summary>
/// Game manager.
/// will collected a player score and try to read an event 
/// what happened in the game and how to do next  
/// </summary>
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SkillManager))]
[RequireComponent(typeof(ItemManager))]
[RequireComponent(typeof(BlackFade))]
public class GameManager : MonoBehaviour
{
	public GUISkin Skin;
	[HideInInspector]
	public int Score = 0;
	[HideInInspector]
	public bool IsPlaying;
	[HideInInspector]
	public int Mode;
	public GameStyle GameView;
	public GameObject GameCamera;
	public GameObject GameCameraTopDown;
	[HideInInspector]
	private GameObject cameraMain;

	void Start ()
	{
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (this.gameObject);
		IsPlaying = true;
	}
	
	void Update ()
	{
		if (cameraMain == null) {
			OrbitGameObject cam = (OrbitGameObject)GameObject.FindObjectOfType (typeof(OrbitGameObject));
			if (cam != null) {
				cameraMain = cam.gameObject;
			} else {
				if (Camera.main == null) {
					switch (GameView) {
					case GameStyle.ThirdPerson:
						if (GameCamera != null) {
							cameraMain = (GameObject)GameObject.Instantiate (GameCamera, Vector3.zero, Quaternion.identity);	
						}
						break;
					case GameStyle.TopDown:
						if (GameCameraTopDown != null) {
							cameraMain = (GameObject)GameObject.Instantiate (GameCameraTopDown, Vector3.zero, Quaternion.identity);	
						}
						break;
					}
				} else {
					cameraMain = Camera.main.camera.gameObject;
				}
			}
		}
	}
	
	// Read message from Game Object and making an event.
	public void GameEvent (string message)
	{
		switch (message) {
		case "endgame":
			Time.timeScale = 0;
			Destroy (GameObject.FindObjectOfType (typeof(PlayerManager)));
			IsPlaying = false;
			Mode = 1;
			break;
		}
	}
	
	void OnGUI ()
	{
		if (Skin != null)
			GUI.skin = Skin;

		if (IsPlaying) {
			Time.timeScale = 1;
			
		} else {
			Time.timeScale = 0;
			Screen.lockCursor = false;
			switch (Mode) {
			case 0:
				GUI.skin.label.fontSize = 25;
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.skin.label.normal.textColor = Color.white;
				GUI.Label (new Rect (0, Screen.height / 2 - 100, Screen.width, 30), "Pause Game");
				if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2, 160, 30), "Save")) {
					PlayerManager player = (PlayerManager)GameObject.FindObjectOfType (typeof(PlayerManager));
					if (player.gameObject.GetComponent<PlayerSave> ())
						player.gameObject.GetComponent<PlayerSave> ().Save ();
					IsPlaying = true;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 40, 160, 30), "Resume")) {
					IsPlaying = true;
					Mode = 0;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 80, 160, 30), "Exit")) {
					PlayerManager player = (PlayerManager)GameObject.FindObjectOfType (typeof(PlayerManager));
					GameObject.Destroy (player.gameObject);
					Application.LoadLevel (PlayerPrefs.GetString ("landingpage"));
					Destroy (this.gameObject);
				}
				break;
			case 1:
				GUI.skin.label.fontSize = 25;
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.skin.label.normal.textColor = Color.white;
				GUI.Label (new Rect (0, Screen.height / 2 - 100, Screen.width, 30), "Game Over");
				if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 40, 160, 30), "Exit")) {
					Application.LoadLevel (PlayerPrefs.GetString ("landingpage"));
					Destroy (this.gameObject);
				}
				
				break;
			}
		
		}
	}
}
public enum GameStyle
{
	TopDown,
	ThirdPerson
}
