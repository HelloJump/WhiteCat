using UnityEngine;
using System.Collections;

public class PlayerCharacterTopdownController : MonoBehaviour
{

	public CharacterSystem character;
	public GameObject Indicator;
	public GameObject Indicator_Attack;
	private Vector3 positionTarget;
	private Transform objectFollowing;
	private GameObject attack_indicator;
	
	// touch screen controller for mobile
	private TouchScreenVal touchScreenMover;
	
	void Start ()
	{	
		touchScreenMover = new TouchScreenVal (new Rect (0, 0, Screen.width, Screen.height));
		// get character instance
		if (character == null) {
			if (this.gameObject.GetComponent<CharacterSystem> ()) {
				character = this.gameObject.GetComponent<CharacterSystem> ();
			}
		}
		
		if (character != null) {
			positionTarget = character.gameObject.transform.position;
		}
		
		if (Indicator_Attack != null) {
			if (attack_indicator == null) {
				attack_indicator = (GameObject)GameObject.Instantiate (Indicator_Attack, Vector3.zero, Indicator_Attack.transform.rotation);
			}
		}
		FindMainCharacter ();
	}

	private float speedMult;

	void Pointing (bool indicated,Vector3 position)
	{
		// poiting function
		Ray ray = Camera.main.ScreenPointToRay (position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 10000)) {
			// check if there's attack able or not
			if (hit.collider.gameObject.GetComponent<DamageManager> () && hit.collider.gameObject != character.gameObject) {
				objectFollowing = hit.collider.gameObject.transform;
			} else {
				objectFollowing = null;
				if (Indicator && indicated) {
					GameObject ind = (GameObject)GameObject.Instantiate (Indicator, hit.point, Indicator.transform.rotation);
					Destroy (ind, 3);
				}
				positionTarget = new Vector3 (hit.point.x, character.gameObject.transform.position.y, hit.point.z);
			}
		}
	}
	
	void FindMainCharacter ()
	{
		if (character == null) {
			PlayerManager player = (PlayerManager)GameObject.FindObjectOfType (typeof(PlayerManager));
			if (player != null && player.GetComponent<CharacterSystem> ()) {
				character = player.gameObject.GetComponent<CharacterSystem> ();
				positionTarget = character.gameObject.transform.position;
			} else {
				PlayerInstance playerIns = (PlayerInstance)GameObject.FindObjectOfType (typeof(PlayerInstance));
				if (playerIns != null && playerIns.GetComponent<CharacterSystem> ()) {
					character = playerIns.gameObject.GetComponent<CharacterSystem> ();
					positionTarget = character.gameObject.transform.position;
				}
			}
		}
	}
	
	void Update ()
	{
		FindMainCharacter ();
		Screen.lockCursor = false;	
		// stop loop if character is not exist.
		if (character == null)
			return;
		
		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		// works only webplayer and desktop devices
		
		// rotation camera
		if (Input.GetButton ("Fire3")) {
			Screen.lockCursor = true;
		}
		// basic input
		if (Input.GetButtonDown ("Fire1")) {
			speedMult = 1;
			Pointing (true,Input.mousePosition);
		}
		if (Input.GetButton ("Fire1")) {
			speedMult = 1;
			Pointing (false,Input.mousePosition);
		}
		// use special attack 
		if (Input.GetButton ("Fire2")) {
			var skillDeployer = character.gameObject.GetComponent<CharacterSkillBase> ();
			if (skillDeployer != null)
				skillDeployer.DeployWithAttacking ();
			speedMult = 0;
			Pointing (true,Input.mousePosition);
		}
		#else
		// works only mobile devices
		Vector2 touchPosition = touchScreenMover.OnTouchPosition();
		if(touchPosition!= Vector2.zero){
			speedMult = 1;
			Pointing (true,new Vector3(touchPosition.x,touchPosition.y,1));
		}
		#endif
		
		
		
		
		// showing indicator to target object
		if (attack_indicator != null) {
			if (objectFollowing != null) {
				attack_indicator.renderer.enabled = true;	
				attack_indicator.transform.position = objectFollowing.transform.position + (Vector3.up * 0.1f);
			} else {
				attack_indicator.renderer.enabled = false;	
			}
		}
		
		// position to go is target object position if it's not a null
		if (objectFollowing != null) {
			positionTarget = new Vector3 (objectFollowing.position.x, character.gameObject.transform.position.y, objectFollowing.position.z);
		}
		
		// check distance to attack
		float distance = Vector3.Distance (positionTarget, character.gameObject.transform.position);
		if (objectFollowing != null && character != null) {
			if (distance <= character.characterStatus.PrimaryWeaponDistance) {
				character.Attack ();
			}
		}
		character.MoveToPosition (positionTarget, speedMult);
	}
}
