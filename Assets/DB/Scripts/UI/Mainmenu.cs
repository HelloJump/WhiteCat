/// <summary>
/// Mainmenu. Just Main menu GUI
/// </summary>
using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour
{

	public Texture2D LogoGame;
	public GUISkin skin;
	public int GameState = 0;
	public GameObject[] TargetLookat;
	public CharacterCreator characterCreator;

	void Start ()
	{
		PlayerPrefs.SetString ("landingpage", Application.loadedLevelName);
		characterCreator = this.gameObject.GetComponent<CharacterCreator> ();
	}

	bool showfeatures;

	void DrawFeatures ()
	{
		if (showfeatures) {
			GameManager gameManage = (GameManager)GameObject.FindObjectOfType (typeof(GameManager));
			GUI.Label (new Rect (20, 20, 120, 30), "Game Mode");
			
			if (GUI.Button (new Rect (20, 50, 120, 30), Application.loadedLevelName)) {
				if (Application.loadedLevelName == "MainMenu_casual") {
					Application.LoadLevel ("MainMenu_creator");
				} else {
					Application.LoadLevel ("MainMenu_casual");
				}
			}
			if (gameManage) {
				GUI.Label (new Rect (20, 80, 120, 30), "Game View");
			
				if (GUI.Button (new Rect (20, 111, 120, 30), gameManage.GameView.ToString ())) {
					if (gameManage.GameView == GameStyle.ThirdPerson) {
						gameManage.GameView = GameStyle.TopDown;		
					} else {
						gameManage.GameView = GameStyle.ThirdPerson;
					}
				}	
			}
			if (GUI.Button (new Rect (20, 150, 120, 30), "Close")) {
				showfeatures = false;
			}
			
		} else {
			if (GUI.Button (new Rect (20, 20, 120, 30), "New Features!")) {
				showfeatures = true;
			}
		}	
	}
	
	void OnGUI ()
	{
		Screen.lockCursor = false;
		if (skin)
			GUI.skin = skin;
		
		
		switch (GameState) {
		case 0:
			DrawFeatures();
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 20, 160, 30), "Start Demo")) {
				if (characterCreator != null) {
					characterCreator.Active = true;
				}
				GameState = 1;
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 60, 160, 30), "Purchase")) {
				Application.OpenURL ("https://www.assetstore.unity3d.com/#/content/10043");
			}
			GUI.DrawTexture (new Rect (Screen.width / 2 - (LogoGame.width * 0.5f) / 2, Screen.height / 2 - 200, LogoGame.width * 0.5f, LogoGame.height * 0.5f), LogoGame);	
		
			break;
		case 1:
			if (characterCreator != null && !characterCreator.Active) {
				GameState = 0;
			}
			if (characterCreator == null) {
				if (GUI.Button (new Rect (50, 50, 160, 30), "Back")) {
					GameState = 0;
				}	
			}
			break;
			
		}
		GUI.skin.label.fontSize = 14;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.normal.textColor = Color.white;
		GUI.Label (new Rect (0, Screen.height - 50, Screen.width, 30), "Dungeon Breaker Starter Kit 2.2.  By Rachan Neamprasert | www.hardworkerstudio.com");
	}

	void Update ()
	{

		if (TargetLookat.Length > 0) {
			this.transform.position = Vector3.Lerp (this.transform.position, TargetLookat [GameState].transform.position, 1.0f * Time.deltaTime);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, TargetLookat [GameState].transform.rotation, 1.0f * Time.deltaTime);	
		}
	}
}
