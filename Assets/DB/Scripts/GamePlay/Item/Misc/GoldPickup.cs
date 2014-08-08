using UnityEngine;
using System.Collections;

public class GoldPickup : MonoBehaviour {

	public int Gold;
	public AudioClip SoundPickup;
	
	void OnTriggerEnter(Collider other) {
		
		if(other.gameObject.GetComponent<PlayerManager>()){
			
       		if(other.gameObject.GetComponent<CharacterInventory>()){
				other.gameObject.GetComponent<CharacterInventory>().Money += Gold;
		
				if(SoundPickup){
					AudioSource.PlayClipAtPoint(SoundPickup,this.transform.position);	
				}
				GameObject.Destroy(this.gameObject.transform.parent.gameObject);	
			}
		}
    }
}
