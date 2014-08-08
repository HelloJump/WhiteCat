using UnityEngine;
using System.Collections;

public class DestructObject : DamageManager
{

	public int HP = 100;
	public GameObject ObjectReplace;
	public GameObject ParticleObject;
	public GameObject[] ItemDropAfterDead;
	public float ItemLifeTime = 5;
	public AudioClip[] SoundHit;
	public AudioClip SoundDestruct;

	void Start ()
	{
	
	}

	public override void AddParticle (Vector3 pos)
	{
		if (ParticleObject) {
			var bloodeffect = (GameObject)Instantiate (ParticleObject, pos, transform.rotation);
			GameObject.Destroy (bloodeffect, 1);	
		}
		base.AddParticle (pos);
	}
	
	public override int ApplayDamage (int damage, Vector3 dirdamge, GameObject attacker)
	{
		if (SoundHit!=null && SoundHit.Length > 0)
			AudioSource.PlayClipAtPoint (SoundHit [Random.Range (0, SoundHit.Length)], this.transform.position);
		
		AddParticle (this.transform.position);
		HP -= damage;
		
		if (HP <= 0) {
			if (SoundDestruct!= null)
				AudioSource.PlayClipAtPoint (SoundDestruct, this.transform.position);
			
			GameObject obj = (GameObject)GameObject.Instantiate (ObjectReplace, this.transform.position, this.transform.rotation);
			GameObject.Destroy (this.gameObject);
			GameObject.Destroy (obj, 5);
			
			if (ItemDropAfterDead != null && ItemDropAfterDead.Length > 0) {
				// Drop item and adding some force
				int randomindex = Random.Range (0, ItemDropAfterDead.Length);
				if (ItemDropAfterDead [randomindex] != null) {
					GameObject item = (GameObject)Instantiate (ItemDropAfterDead [randomindex], this.gameObject.transform.position + Vector3.up * 2, this.gameObject.transform.rotation);
					if (item.rigidbody) {
						item.rigidbody.AddForce ((-this.transform.forward + Vector3.up) * 100);
						item.rigidbody.AddTorque ((-this.transform.forward + Vector3.up) * 100);	
					}
					GameObject.Destroy (item, ItemLifeTime);
				}
			}
			
		}
		return damage;
	}

}
