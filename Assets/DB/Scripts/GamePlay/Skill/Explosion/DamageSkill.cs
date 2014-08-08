/// <summary>
/// Damage skill.
/// will ApplayDamage() to <CharacterStatus>Object by around the radius
/// </summary>
using UnityEngine;
using System.Collections;

public class DamageSkill : SkillBase {

	public int Force;
	public float Radius;
	
	
	void Start () {
        var colliders = Physics.OverlapSphere(this.transform.position, Radius);
        foreach(var hit in colliders)
		{
            if(!hit)
            	continue;
			
			bool hited = false;
			for(int i=0;i<TagDamage.Length;i++){
				if(hit.tag == TagDamage[i]){	
					hited = true;
				}
			}
			
			if(hited){	
				if(hit.gameObject.GetComponent<DamageManager>()){
					hit.gameObject.GetComponent<DamageManager>().ApplayDamage(Damage,Vector3.zero,Owner);
				}
			}
			
			if (hit.rigidbody){
                hit.rigidbody.AddExplosionForce(Force, transform.position, Radius, 3.0f);
			}
        }
	}

}
