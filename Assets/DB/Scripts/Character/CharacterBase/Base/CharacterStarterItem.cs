/// <summary>
/// Character starter item. Using for adding an item into your character
/// </summary>
using UnityEngine;
using System.Collections;

public class CharacterStarterItem : MonoBehaviour
{
	public bool Active = true;
	public int[] StarterItem;
	
	void Start ()
	{
		if(Active){
			Setting();
		}
	}
	
	public void Setting(){
		CharacterInventory character = this.gameObject.GetComponent<CharacterInventory> ();
		character.RemoveAllItem();
		if (character) {
			for (int i=0; i<StarterItem.Length; i++) {
				character.AddItem (StarterItem [i], 1);
			}
		}
	}

}
