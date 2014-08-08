using UnityEngine;
using System.Collections;

public class CharacterCreator : MonoBehaviour
{

	public GUISkin skin;
	public bool Active = false;
	public GameObject CharacterStarter;
	public GameObject[] CharacterBase;
	public CharacterSelector CharacterLauncher;
	private bool createMode;
	private string newCharacterName = "Enter Your Name";
	private string[] characterList;
	private Vector2 scrollPosition;
	private bool characterLoaded;
	private int indexModel;
	private bool removeConfirm;

	void Start ()
	{
		//load all characters data to the lists
		loadCharacterList ();
	}

	void SetCharacter (int index)
	{
		indexModel = index;
		GameObject.Destroy (CharacterStarter);
		// clone character prefab to CharacterStarter
		CharacterStarter = (GameObject)GameObject.Instantiate (CharacterBase [indexModel], CharacterLauncher.transform.position, CharacterLauncher.transform.rotation);
		if (CharacterStarter.GetComponent<CharacterStarterItem> ()) {
			CharacterStarter.GetComponent<CharacterStarterItem> ().Active = false;
		}		
	}

	void OnGUI ()
	{
		if (!Active)
			return;
		
		if (skin)
			GUI.skin = skin;
		
		if (CharacterStarter) {
			if (createMode) {
				newCharacterName = GUI.TextField (new Rect (50, 100, 160, 39), newCharacterName);
				CharacterStarterItem characterItem = CharacterStarter.GetComponent<CharacterStarterItem> ();
				if (characterItem) {
					if (GUI.Button (new Rect (50, 160, 160, 30), "Sword Man")) {
						characterItem.Active = true;
						characterItem.StarterItem [0] = 1;
						characterItem.Setting ();
					}
					if (GUI.Button (new Rect (50, 200, 160, 30), "Berserker")) {
						characterItem.Active = true;
						characterItem.StarterItem [0] = 0;
						characterItem.Setting ();
					}
					if (GUI.Button (new Rect (50, 240, 160, 30), "Wizard")) {
						characterItem.Active = true;
						characterItem.StarterItem [0] = 5;
						characterItem.Setting ();
					}
					if (GUI.Button (new Rect (50, 280, 50, 30), "1")) {
						SetCharacter (0);
						if (CharacterStarter.GetComponent<CharacterStarterItem> ())
							CharacterStarter.GetComponent<CharacterStarterItem> ().Setting ();
					}
					if (GUI.Button (new Rect (110, 280, 50, 30), "2")) {
						SetCharacter (1);
						if (CharacterStarter.GetComponent<CharacterStarterItem> ())
							CharacterStarter.GetComponent<CharacterStarterItem> ().Setting ();
					}
				
					if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 140, 160, 30), "Ok")) {
						CharacterStarter.GetComponent<CharacterStatus> ().ModelIndex = indexModel;
						if (CharacterStarter.GetComponent<PlayerSave> ().NewCharacter (newCharacterName)) {
							createMode = false;
							loadCharacterList ();
						}
					}
					if (GUI.Button (new Rect (50, Screen.height - 140, 160, 30), "Cancel")) {
						loadCharacterList ();
						createMode = false;
					}
				}

			} else {
				GUI.skin.label.fontSize = 20;
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.skin.label.normal.textColor = Color.white;
				GUI.Label (new Rect (50, 50, 160, 30), "Character List");
				// Render all characters
				if (characterList != null) {
					scrollPosition = GUI.BeginScrollView (new Rect (50, 100, 180, Screen.height - 400), scrollPosition, new Rect (0, 0, 160, characterList.Length * 35));
					for (int i=0; i<characterList.Length; i++) {
						if (characterList [i] != "") {
							if (GUI.Button (new Rect (0, 35 * i, 160, 30), characterList [i])) {
								SetCharacter (CharacterStarter.GetComponent<PlayerSave> ().LoadIndexModel (characterList [i]));
								CharacterStarter.GetComponent<CharacterStatus> ().Name = characterList [i];
								CharacterStarter.GetComponent<PlayerSave> ().Load ();
							}
						}
					}
					GUI.EndScrollView ();
				}
				
				if (characterLoaded) {
					if (removeConfirm) {
						if (GUI.Button (new Rect (50, Screen.height - 280, 75, 30), "Confirm")) {
							CharacterStarter.GetComponent<PlayerSave> ().RemoveCharacter ();
							createMode = false;
							loadCharacterList ();
						}
						if (GUI.Button (new Rect (135, Screen.height - 280, 75, 30), "Cancel")) {
							removeConfirm = false;
						}
					} else {
						if (GUI.Button (new Rect (50, Screen.height - 280, 160, 30), "Remove Character")) {
							removeConfirm = true;
						}	
					}
				}
				
				if (GUI.Button (new Rect (50, Screen.height - 240, 160, 30), "Create Character")) {
					createMode = true;	
				}
				
				if (characterLoaded) {
					if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 140, 160, 30), "Start")) {
						CharacterLauncher.CharacterSlected = CharacterStarter;
						GameObject.DontDestroyOnLoad (CharacterLauncher.CharacterSlected);
						CharacterLauncher.StartThisCharacter (true);
					}
				}
				
				if (GUI.Button (new Rect (50, Screen.height - 140, 160, 30), "Main Menu")) {
					Active = false;
				}
			}
		}
		
	}

	void loadCharacterList ()
	{
		// load all save characters to characterList[]
		characterLoaded = false;
		characterList = PlayerPrefs.GetString ("GAME_CHARACTER").Split ("|" [0]);
		if (characterList [0] != "") {
			// set starter chatacter. characterList[0] will show up first.
			SetCharacter (CharacterStarter.GetComponent<PlayerSave> ().LoadIndexModel (characterList [0]));
			CharacterStarter.GetComponent<CharacterStatus> ().Name = characterList [0];
			CharacterStarter.GetComponent<PlayerSave> ().Load ();
			characterLoaded = true;
		}
	}
}
