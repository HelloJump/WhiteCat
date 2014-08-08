/// <summary>
/// Player character controller.
/// Player Controller by Keyboard and Mouse
/// </summary>
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerCharacterController : MonoBehaviour
{
	
	public CharacterSystem character;
	// touch screen controller
	private TouchScreenVal touchScreenMover;
	private TouchScreenVal touchScreenPress;
	
	void Start ()
	{
		//setting touch screen controller. (Specify area of touching and position)
		touchScreenMover = new TouchScreenVal (new Rect (0, 0, Screen.width / 2, Screen.height));
		touchScreenPress = new TouchScreenVal (new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height));
		
		// get character instance
		if (character == null) {
			if (this.gameObject.GetComponent<CharacterSystem> ()) {
				character = this.gameObject.GetComponent<CharacterSystem> ();
			}
		}
		
		Screen.lockCursor = true;
		FindMainCharacter();
	}
	
	void FindMainCharacter(){
		if (character == null) {
			PlayerManager player = (PlayerManager)GameObject.FindObjectOfType (typeof(PlayerManager));
			if (player != null && player.GetComponent<CharacterSystem> ()) {
				character = player.gameObject.GetComponent<CharacterSystem> ();
			} else {
				PlayerInstance playerIns = (PlayerInstance)GameObject.FindObjectOfType (typeof(PlayerInstance));
				if (playerIns != null && playerIns.GetComponent<CharacterSystem> ()) {
					character = playerIns.gameObject.GetComponent<CharacterSystem> ();
				}
			}
		}
	}
	
	void Update ()
	{
		FindMainCharacter();
		if (!character)
			return;
			
			#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
			// works only webplayer and desktop devices
			var direction	= Vector3.zero;
			var forward	= Quaternion.AngleAxis(-90,Vector3.up) * Camera.main.transform.right;
		
			direction	+= forward * Input.GetAxis("Vertical");
			direction	+= Camera.main.transform.right * Input.GetAxis("Horizontal");
			
			var orbit = (OrbitGameObject)FindObjectOfType(typeof(OrbitGameObject));
			
			if(Input.GetButton("Fire3"))
    		{
				orbit.HoldAim();
			}
            if(Screen.lockCursor){	
				if(Input.GetButton("Fire1"))
    			{
					character.Attack();
				}		
				if(Input.GetButton("Fire2"))
    			{
					character.Attack();
					var skillDeployer = character.gameObject.GetComponent<CharacterSkillBase>();
					if(skillDeployer != null)
						skillDeployer.DeployWithAttacking();	
				}
			}
		
			if (Screen.lockCursor && Input.GetKeyDown (KeyCode.Tab)) {
				Screen.lockCursor = false;	
			}
		
			direction.Normalize();
			character.Move(direction.normalized,1);
			if(direction.magnitude>0){
				if(!Screen.lockCursor)
				{
					Screen.lockCursor = true;
				}
			}
		#else
		// works only mobile devices
		mobileController ();
		#endif
	}
	
	
	// Touchscreen controller function 
	void mobileController ()
	{
		if (!character)
			return;
		touchScreenPress.AreaTouch = new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height / 2);
		if (touchScreenPress.OnTouchPress ()) {
			character.Attack ();	
		}
		touchScreenPress.AreaTouch = new Rect (Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2);
		if (touchScreenPress.OnTouchPress ()) {
			character.Attack ();
			var skillDeployer = character.gameObject.GetComponent<CharacterSkillBase> ();
			if (skillDeployer != null)
				skillDeployer.DeployWithAttacking ();	
		}
		
		
		var direction = Vector3.zero;
		var touchDirection = touchScreenMover.OnTouchDirection ();
		direction.x = touchDirection.x;
		direction.z = touchDirection.y;
		
		character.Move (direction, 1);
	}

	
}

