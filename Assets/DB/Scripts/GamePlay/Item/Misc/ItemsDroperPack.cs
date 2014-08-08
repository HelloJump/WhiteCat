/// <summary>
/// Items droper pack. using to spawn multiple items or objects 
/// </summary>
using UnityEngine;
using System.Collections;

public class ItemsDroperPack : MonoBehaviour {

	public GameObject Item;
	public int Number;
	public bool RandomNum;
	public float Force = 50;
	public AudioClip Sounds;
	public int LifeTimeItem = 5;
	
	void Start () {
		if(!Item)
			return;
		
		if(RandomNum)
			Number = Random.Range(0,Number);
		
		if(Sounds){
			AudioSource.PlayClipAtPoint(Sounds,this.transform.position);	
		}
		
		for(int i=0;i<Number;i++){
			if(Item!=null){
				GameObject item = (GameObject)Instantiate(Item,this.gameObject.transform.position+ Vector3.up,Random.rotation);
				if(item.rigidbody){
					item.rigidbody.AddForce((-this.transform.forward + Vector3.up) * Force);
					item.rigidbody.AddTorque((-this.transform.forward + Vector3.up) * Force);	
				}
				GameObject.Destroy(item,LifeTimeItem);
				
			}
		}
		GameObject.Destroy(this.gameObject);
	}

}
