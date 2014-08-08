// Save all character data
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterSystem))]

public class PlayerSave : MonoBehaviour
{
	private CharacterSystem character;
	
	void Awake ()
	{
		character = this.GetComponent<CharacterSystem> ();
	}
	void Start(){
	}
	
	public bool NewCharacter (string Name)
	{
		bool res = true;
		string[] characterlist = PlayerPrefs.GetString ("GAME_CHARACTER").Split ("|" [0]);
		for (int i=0; i<characterlist.Length; i++) {
			if (characterlist [i] == Name) {
				res = false;
				break;	
			}
		}
		if (res) {
			character.characterStatus.Name = Name;
			PlayerPrefs.SetString ("GAME_CHARACTER", PlayerPrefs.GetString ("GAME_CHARACTER") + Name + "|");
			Save ();
		}
		Debug.Log (PlayerPrefs.GetString ("GAME_CHARACTER"));
		return res;
	}
	
	public bool RemoveCharacter ()
	{
		bool res = false;
		string[] characterlist = PlayerPrefs.GetString ("GAME_CHARACTER").Split ("|" [0]);
		for (int i=0; i<characterlist.Length; i++) {
			if (characterlist [i] == character.characterStatus.Name) {
				characterlist [i] = "";	
				res = true;
				break;
			}
		}
		if (res) {
			string list = "";
			for (int i=0; i<characterlist.Length; i++) {
				if (characterlist [i] != "") {
					list += characterlist [i] + "|";	
				}
			}
			PlayerPrefs.SetString ("GAME_CHARACTER", list);
			
		}
		Debug.Log (PlayerPrefs.GetString ("GAME_CHARACTER"));
		return res;
	}
	
	public void Save ()
	{
		if(!character)
			return;

		string namesave = "_" + character.characterStatus.Name;
		// save status
		PlayerPrefs.SetString ("SAVEDATE" + namesave, System.DateTime.Now.ToString ());
		PlayerPrefs.SetInt ("STR" + namesave, character.characterStatus.STR);
		PlayerPrefs.SetInt ("AGI" + namesave, character.characterStatus.AGI);
		PlayerPrefs.SetInt ("INT" + namesave, character.characterStatus.INT);
		PlayerPrefs.SetInt ("LEVEL" + namesave, character.characterStatus.LEVEL);
		PlayerPrefs.SetInt ("EXP" + namesave, character.characterStatus.EXP);
		PlayerPrefs.SetInt ("EXPmax" + namesave, character.characterStatus.EXPmax);
		PlayerPrefs.SetInt ("HP" + namesave, character.characterStatus.HP);
		PlayerPrefs.SetInt ("SP" + namesave, character.characterStatus.SP);
		PlayerPrefs.SetInt ("MODELINDEX" + namesave, character.characterStatus.ModelIndex);
		PlayerPrefs.SetInt ("MONEY" + namesave, character.characterInventory.Money);
		
		string itemIndex = "";
		string itemNum = "";
		for (int i=0; i<character.characterInventory.ItemSlots.Count; i++) {
			if (character.characterInventory.ItemSlots [i] != null) {
				itemIndex += character.characterInventory.ItemSlots [i].Index + ",";
				itemNum += character.characterInventory.ItemSlots [i].Num + ",";
			}
		}
		PlayerPrefs.SetString ("ItemsIndex" + namesave, itemIndex);
		PlayerPrefs.SetString ("ItemsNum" + namesave, itemNum);
		
		string itemEqIndex = "";
		for (int i=0; i<character.characterInventory.ItemsEquiped.Length; i++) {
			if (character.characterInventory.ItemsEquiped [i] != null) {
				itemEqIndex += character.characterInventory.ItemsEquiped [i].Index + ",";
			}
		}
		PlayerPrefs.SetString ("ItemsEqIndex" + namesave, itemEqIndex);
		Debug.Log ("Save Game");
	}
	
	public int LoadIndexModel(string name){
		string namesave = "_" + name;
		return PlayerPrefs.GetInt ("MODELINDEX" + namesave);
	}
	
	public void Load ()
	{
		if(!character)
			return;
		
		
		string namesave = "_" + character.characterStatus.Name;
		if (PlayerPrefs.GetString ("SAVEDATE" + namesave) == "") {
			Debug.Log ("No Save Game");
			return;
		}
		Debug.Log ("Load Game");
		
		character.characterStatus.STR = PlayerPrefs.GetInt ("STR" + namesave);
		character.characterStatus.AGI = PlayerPrefs.GetInt ("AGI" + namesave);
		character.characterStatus.INT = PlayerPrefs.GetInt ("INT" + namesave);
		character.characterStatus.LEVEL = PlayerPrefs.GetInt ("LEVEL" + namesave);
		character.characterStatus.EXP = PlayerPrefs.GetInt ("EXP" + namesave);
		character.characterStatus.EXPmax = PlayerPrefs.GetInt ("EXPmax" + namesave);
		character.characterStatus.HP = PlayerPrefs.GetInt ("HP" + namesave);
		character.characterStatus.SP = PlayerPrefs.GetInt ("SP" + namesave);
		character.characterStatus.ModelIndex = PlayerPrefs.GetInt ("MODELINDEX" + namesave);
		character.characterInventory.Money = PlayerPrefs.GetInt ("MONEY" + namesave);
		
		string[] itemIndex = PlayerPrefs.GetString ("ItemsIndex" + namesave).Split ("," [0]);
		string[] itemNum = PlayerPrefs.GetString ("ItemsNum" + namesave).Split ("," [0]);
		string[] itemEqIndex = PlayerPrefs.GetString ("ItemsEqIndex" + namesave).Split ("," [0]);

		character.characterInventory.ItemSlots.Clear ();
		for (int i=0; i<itemIndex.Length; i++) {
			if (itemIndex [i] != "") {
				ItemSlot itemS = new ItemSlot ();
				itemS.Index = int.Parse (itemIndex [i]);
				itemS.Num = int.Parse (itemNum [i]);
				character.characterInventory.ItemSlots.Add (itemS);
			}
		}
		character.characterInventory.UnEquipAll ();
		for (int i=0; i<itemEqIndex.Length; i++) {
			if (itemEqIndex [i] != null && itemEqIndex [i] != "") {
				ItemSlot itemQe = new ItemSlot ();
				itemQe.Index = int.Parse (itemEqIndex [i]);
				character.characterInventory.EquipItem (itemQe);
			}
		}
		
	}

}
