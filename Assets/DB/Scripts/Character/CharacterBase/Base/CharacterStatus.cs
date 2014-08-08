/// <summary>
/// Character status. is the Character Structure contain a basic Variables 
/// such as HP , SP , Defend , Name or etc... and you can adding more.
///  - this class will calculate all character Status. ex. Hp regeneration per sec
///  - Checking any event. ex. ApplyDamage , Dead , etc...
///  - After the Character is dead will replaced with Dead body or Ragdoll object [DeadbodyModel]
/// </summary>
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class CharacterStatus : DamageManager
{
	public GameObject DeadbodyModel;
	public GameObject ParticleObject;
	public GameObject LevelUpFx;
	public GameObject Projectile;
	public Texture2D ThumbnailImage;
	public AudioClip[] SoundHit;
	public AudioClip[] SoundLaunch;
	public GameObject FloatingText;
	public string Name = "Player";
	[HideInInspector]
	public int HP = 10;
	[HideInInspector]
	public int SP = 10;
	[HideInInspector]	
	public int SPmax = 10;
	[HideInInspector]
	public int HPmax = 10;
	public int BaseHPmax = 10;
	public int BaseSPmax = 10;
	public int BaseDamage = 1;
	public int BaseDefend = 1;
	
	[HideInInspector]
	public float AttackSpeed = 0.2f;
	[HideInInspector]
	public float AttackSpeedInventory = 0;
	[HideInInspector]
	public int Damage = 2;
	[HideInInspector]
	public int Defend = 2;
	[HideInInspector]
	public int DamageInventory = 2;
	[HideInInspector]
	public int DefendInventory = 2;
	[HideInInspector]
	public int ModelIndex;
	
	public int HPregen = 1;
	public int SPregen = 1;
	public int EXPgive = 2;
	public int LEVEL = 1;
	public int STR = 2;
	public int AGI = 2;
	public int INT = 2;
	public int STRPlus = 1;
	public int AGIPlus = 1;
	public int INTPlus = 1;
	public int EXP = 0;
	public int EXPmax = 2;
	public int EXPPlus = 50;
	public int StatusPoint = 0;
	public int StatusPointPlus = 3;
	public WeaponType PrimaryWeaponType = WeaponType.Melee;
	public float PrimaryWeaponDistance = 2;
	public bool ForceSpeedAttackByAGI = true;
	private GameObject killer;
	private Vector3 velocityDamage;
	private float lastRegen;
	private CharacterSystem character;
	
	void Start(){
		character = gameObject.GetComponent<CharacterSystem>();	
	}
	
	void Update ()
	{
		if (Time.time - lastRegen >= 1) {
			lastRegen = Time.time;
			HP += HPregen;
			SP += SPregen;
		}
		SPmax = BaseSPmax + (INT * 3);
		HPmax = BaseHPmax + (STR * 5);
		
		if (HP > HPmax)
			HP = HPmax;	

		if (SP > SPmax)
			SP = SPmax;
		
		Damage = (STR * 2) + BaseDamage + DamageInventory;
		Defend = BaseDefend + DefendInventory;
		
		if (AttackSpeedInventory > 0) {
			if(ForceSpeedAttackByAGI){
				AttackSpeed = AttackSpeedInventory + (AGI * 0.1f);
			}else{
				AttackSpeed = AttackSpeedInventory;
			}
		} else {
			if(ForceSpeedAttackByAGI){
				AttackSpeed = (AGI * 0.1f);
			}else{
				AttackSpeed = 0;	
			}
		}
		if(AttackSpeed<0){
			AttackSpeed = 0.01f;	
		}
		if (character)
			character.speedAttack = AttackSpeed;
	}
	
	public void ApplayEXP (int expgot)
	{
		EXP += expgot;
		while (EXP >= EXPmax) {
			LevelUp ();
		}
	}
	
	public void LevelUp ()
	{
		EXP -= EXPmax;
		EXPmax += EXPPlus;
		LEVEL += 1;
		StatusPoint += StatusPointPlus;
		STR += STRPlus;
		AGI += AGIPlus;
		INT += INTPlus;
		
		var skillDeployer = this.gameObject.GetComponent<CharacterSkillBase> ();
		if (skillDeployer != null)
			skillDeployer.SkillPoint += 1;	
		
		if (LevelUpFx) {
			GameObject lvup = (GameObject)GameObject.Instantiate (LevelUpFx, this.gameObject.transform.position, Quaternion.identity);	
			lvup.gameObject.transform.parent = this.gameObject.transform;
		}
	}
	
	public void GiveExpToKiller ()
	{
		if (killer) {
			if (killer.GetComponent<CharacterStatus> ()) {
				killer.GetComponent<CharacterStatus> ().ApplayEXP (EXPgive);
			}
		}
	}
	
	public override int ApplayDamage (int damage, Vector3 dirdamge, GameObject attacker)
	{	
		// Applay Damage function
		if (HP < 0) {
			return 0;	
		}
		if (SoundHit != null && SoundHit.Length > 0) {
			int randomindex = Random.Range (0, SoundHit.Length);
			if (SoundHit [randomindex] != null) {
				AudioSource.PlayClipAtPoint (SoundHit [randomindex], this.transform.position);	
			}
		}
		if (character) {
			character.GotHit (1);
		}
		var damval = damage - Defend;
		if (damval < 1) {
			damval = 1;	
		}
		killer = attacker;
		HP -= damval;
		AddFloatingText (this.transform.position, damval.ToString ());
		velocityDamage = dirdamge;
		if (HP <= 0) {
			Dead ();
		}
		return damval;
	}
	
	public override void AddParticle (Vector3 pos)
	{
		if (ParticleObject) {
			var bloodeffect = (GameObject)Instantiate (ParticleObject, pos, transform.rotation);
			GameObject.Destroy (bloodeffect, 1);	
		}
		base.AddParticle (pos);
	}

	public void AddFloatingText (Vector3 pos, string text)
	{
		// Adding Floating Text Effect
		if (FloatingText) {
			var floattext = (GameObject)Instantiate (FloatingText, pos, transform.rotation);
			if (floattext.GetComponent<FloatingText> ()) {
				floattext.GetComponent<FloatingText> ().Text = text;
			}
			GameObject.Destroy (floattext, 1);	
		}
	}
	
	void Dead ()
	{
		if (DeadbodyModel) {
			var deadbody = (GameObject)Instantiate (DeadbodyModel, this.gameObject.transform.position, this.gameObject.transform.rotation);
			CopyTransformsRecurse (this.gameObject.transform, deadbody.transform);
			Destroy (deadbody, 10.0f);
		}
		if (this.GetComponent<CharacterDie> ()) {
			this.GetComponent<CharacterDie> ().OnDead ();
		}
		GiveExpToKiller ();
		Destroy (gameObject);
	}
	
	public void CopyTransformsRecurse (Transform src, Transform dst)
	{
		// Copy all transforms to Dead object replacement (Ragdoll)
		dst.position = src.position;
		dst.rotation = src.rotation;
		dst.localScale = src.localScale;
		foreach (var child in dst.Cast<Transform>()) {
			var curSrc = src.Find (child.name);
			if (child.rigidbody)
				child.rigidbody.AddForce (velocityDamage / 3f);

			if (curSrc)
				CopyTransformsRecurse (curSrc, child);
		}
	}

}

