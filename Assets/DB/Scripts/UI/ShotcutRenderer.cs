using UnityEngine;
using System.Collections;

public struct ShotcutSlot
{
	public int Type;
	public int Index;
	public bool Active;
	public KeyCode Key;
	public float cooldown;
	public float timepress;
}

public class ShotcutRenderer : Frame
{
	
	public ShotcutSlot[] Shotcut;
	private ItemManager itemManage;
	private SkillManager skillManage;
	
	public void AddShopCut (int index, int type, float cooldown)
	{
		
		for (int i=0; i<Shotcut.Length; i++) {
			if (Shotcut [i].Active) {
				if (Shotcut [i].Index == index && Shotcut [i].Type == type) {
					return;
				}
			}
		}
		
		for (int i=0; i<Shotcut.Length; i++) {
			if (!Shotcut [i].Active) {
				Shotcut [i].Active = true;
				Shotcut [i].Index = index;
				Shotcut [i].Type = type;
				Shotcut [i].cooldown = cooldown;
				return;
			}
		}
	}
	
	public override void Inz (Vector2 position, PlayerCharacterUI ui)
	{
		Shotcut = new ShotcutSlot[5];
		Shotcut [0].Key = KeyCode.Alpha1;
		Shotcut [1].Key = KeyCode.Alpha2;
		Shotcut [2].Key = KeyCode.Alpha3;
		Shotcut [3].Key = KeyCode.Alpha4;
		Shotcut [4].Key = KeyCode.Alpha5;
		
		skillManage = (SkillManager)UnityEngine.GameObject.FindObjectOfType (typeof(SkillManager));
		itemManage = (ItemManager)UnityEngine.GameObject.FindObjectOfType (typeof(ItemManager));
		base.Inz (position, ui);
	}
	
	public void Draw ()
	{
		GUI.BeginGroup (new Rect (Position.x, Position.y, Shotcut.Length * 60, 80));
		for (int i=0; i<Shotcut.Length; i++) {
			if (Shotcut [i].Active) {
				switch (Shotcut [i].Type) {
				case 0:
					DrawItem (i, new Vector2 (i * 60, 18), PlayerUI.characterInventory);
					break;
				case 1:
					DrawSkill (i, new Vector2 (i * 60, 18), PlayerUI.characterSkill);
					break;
				}
			} else {
				GUI.Box (new Rect (i * 60, 18, 50, 50), "");	
			}
			if (GUI.Button (new Rect (i * 60, 0, 50, 15), "x")) {
				Shotcut [i].Active = false;
			}
		}
		
		GUI.EndGroup ();
		
		
		
		int indexofpotion = 4;// index of potion in itemManager;
		int index = PlayerUI.character.characterInventory.FindSlotIndexFromItem(indexofpotion);
		if (index != -1) {
			var item = PlayerUI.character.characterInventory.GetItemFromSlot(index);
			string num = PlayerUI.character.characterInventory.ItemSlots [index].Num.ToString ();
			if (GUI.Button (new Rect (Screen.width - 80, Screen.height - 80, 50, 50), item.Icon)) {
				PlayerUI.character.characterInventory.UseItem (PlayerUI.character.characterInventory.ItemSlots [index]);
			}
			
			GUI.skin.label.fontSize = 13;
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			GUI.Label (new Rect (4 + Screen.width - 80, 4 + Screen.height - 80, 30, 30), num);
			
		}
	}
	
	public void DrawItem (int index, Vector2 position, CharacterInventory characterInventory)
	{
		if (!characterInventory || !itemManage || Shotcut [index].Index >= characterInventory.ItemSlots.Count) {
			Shotcut [index].Active = false;
			return;
			
		}
		var itemslot = characterInventory.ItemSlots [Shotcut [index].Index];

		if (itemslot != null) {
			var item = itemManage.Items [itemslot.Index];

			if (GUI.Button (new Rect (position.x, position.y, 50, 50), item.Icon) || Input.GetKeyDown (Shotcut [index].Key)) {
				if (Time.time > Shotcut [index].timepress + Shotcut [index].cooldown) {
					switch (item.ItemType) {
					case ItemType.Weapon:
						if (characterInventory.CheckEquiped (itemslot)) {
							characterInventory.UnEquipItem (itemslot);
						} else {
							characterInventory.EquipItem (itemslot);	
						}
						break;
					case ItemType.Edible:
						characterInventory.UseItem (itemslot);
						break;
				
					}
					Shotcut [index].timepress = Time.time;
				}
			}
			GUI.skin.label.normal.textColor = Color.white;

			if (itemslot.Num > 0) {
				GUI.skin.label.fontSize = 13;
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
				GUI.Label (new Rect (4 + position.x, 4 + position.y, 30, 30), itemslot.Num.ToString ());
			}
		}
		if (itemslot == null) {
			Shotcut [index].Active = false;	
		}
	}
	
	public void DrawSkill (int index, Vector2 position, CharacterSkillManager characterSkill)
	{

		if (!characterSkill || !skillManage) {
			Shotcut [index].Active = false;
			return;
			
		}
		
		if (GUI.Button (new Rect (position.x, position.y, 50, 50), skillManage.Skills [characterSkill.SkillIndex [Shotcut [index].Index]].SkillIcon) || Input.GetKeyDown (Shotcut [index].Key)) {
			if (Time.time > Shotcut [index].timepress + Shotcut [index].cooldown) {
				characterSkill.indexSkill = characterSkill.SkillIndex [Shotcut [index].Index];
				characterSkill.DeployWithAttacking ();
				Shotcut [index].timepress = Time.time;
			}
		}
		
		// Freeze cooldown before mana is enough
		
		/*int manacost = characterSkill.skillManage.GetManaCost (characterSkill.SkillIndex [Shotcut [index].Index], characterSkill.SkillLevel [Shotcut [index].Index]);
		if (characterSkill.character.SP < manacost) {
			characterSkill.cooldownTemp [characterSkill.SkillIndex [Shotcut [index].Index]] = Time.time;
		}*/
		
		
		GUI.skin.label.fontSize = 13;
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
		
		float cooldown = 0;
		if (characterSkill.cooldownTemp.Length > 0) {
			cooldown = ((characterSkill.skillManage.GetCooldown (characterSkill.SkillIndex [Shotcut [index].Index], characterSkill.SkillLevel [Shotcut [index].Index]) + characterSkill.cooldownTemp [characterSkill.SkillIndex [Shotcut [index].Index]]) - Time.time);
		}
		if (cooldown > 0) {
			GUI.skin.label.normal.textColor = Color.red;
			GUI.Label (new Rect (4 + position.x, 4 + position.y, 50, 30), cooldown.ToString ("f1") + " s.");
		} else {
			GUI.skin.label.normal.textColor = Color.white;
			GUI.Label (new Rect (4 + position.x, 4 + position.y, 30, 30), characterSkill.SkillLevel [Shotcut [index].Index].ToString ());
		}
	}
}
