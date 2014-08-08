using UnityEngine;
using System.Collections;

public class OrbitGameObject : Orbit
{
	public GameObject Target;
	public GameObject CameraObject;
	public Vector3 TargetOffset = Vector3.zero;
	public Vector3 CameraPositionZoom;
	public float CameraLength;
	public float CameraLengthZoom;
	public Vector2 OrbitSpeed = new Vector2 (0.01f, 0.01f);
	public Vector2 OrbitOffset = new Vector2 (0, -0.8f);
	[HideInInspector]
	public Vector3 ShakeVal = Vector3.zero;
	private float zoomVal;
	private Vector3 cameraPositiontemp;
	private Vector3 cameraPosition;
	public bool CameraCollision = true;
	public bool IgnoreTrigger = false;

	void Start ()
	{
		Data.Zenith = -0.4f;
		Data.Length = CameraLength;

		if (CameraObject) {
			cameraPositiontemp = CameraObject.transform.localPosition;
			cameraPosition = cameraPositiontemp;
		}
		Screen.lockCursor = true;
	}
	
	public void HoldAim ()
	{
		if (CameraObject) {
			cameraPosition = CameraPositionZoom;	
			zoomVal = CameraLengthZoom;
			
		}
	}

	protected override void Update ()
	{
		
		if (CameraObject) {
			CameraObject.transform.localPosition = Vector3.Lerp (CameraObject.transform.localPosition, cameraPosition, 10.0f * Time.deltaTime);
		}	
#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		if(Screen.lockCursor){
			Data.Azimuth += Input.GetAxis("Mouse X") * OrbitSpeed.x;
			Data.Zenith	+= Input.GetAxis("Mouse Y") * OrbitSpeed.y;
		}
#endif
		
		RaycastHit hit;
		float distanceToObject = -zoomVal;
		if (CameraCollision) {
			if (Target != null) {
				if (Physics.Raycast (Target.transform.position + TargetOffset, -this.transform.forward, out hit)) {
					if (hit.collider.gameObject != this.gameObject && hit.collider.gameObject != Target.gameObject && !hit.collider.gameObject.GetComponent<CharacterSystem>() && ((!IgnoreTrigger && !hit.collider.isTrigger) || IgnoreTrigger)) {
						if (hit.distance < distanceToObject) {
							distanceToObject = hit.distance;
						
						}
					}
				}
			}
		}
		Data.Zenith = Mathf.Clamp (Data.Zenith + OrbitOffset.x, OrbitOffset.y, 0f);
		float deltadistance = Mathf.Clamp (zoomVal, -distanceToObject, -distanceToObject);
		Data.Length += (deltadistance - Data.Length) / 10;
		
		
		Time.timeScale += (1 - Time.timeScale) / 10f;
		var lookAt = TargetOffset + ShakeVal;
		
		if (Target == null) {
			PlayerInstance characterInstance = (PlayerInstance)FindObjectOfType (typeof(PlayerInstance));
			if (characterInstance) {
				Target = characterInstance.gameObject;	
				return;
			}
			PlayerManager character = (PlayerManager)FindObjectOfType (typeof(PlayerManager));
			if (character) {
				Target = character.gameObject;
				return;
			}
		}
		
		if (Target != null) {
			lookAt += Target.transform.position;
			base.Update ();
			gameObject.transform.position += lookAt;
			gameObject.transform.LookAt (lookAt);
			if (zoomVal == CameraLengthZoom) {
				Quaternion targetRotation = this.transform.rotation;
				targetRotation.x = 0;
				targetRotation.z = 0;
				Target.transform.rotation = targetRotation;
			}
		}
		
		cameraPosition = cameraPositiontemp;
		zoomVal = CameraLength;
		rotationXtemp = Data.Azimuth;
		rotationYtemp = Data.Zenith;
	}
	

	// Rotation screen with touchs
	
	private float rotationXtemp = 0;
	private float rotationYtemp = 0;
	private Vector2 controllerPositionTemp;
	private Vector2 controllerPositionNext;
	
	void mobileController ()
	{
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).position.x < Screen.width / 2) {
				if (Input.GetTouch (i).phase == TouchPhase.Began || Input.GetTouch (i).phase == TouchPhase.Stationary) {
                	
				}	
			} else {	
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					controllerPositionNext = new Vector2 (Input.GetTouch (i).position.x, Screen.height - Input.GetTouch (i).position.y);
					controllerPositionTemp = controllerPositionNext;
					
				} else {
					controllerPositionNext = new Vector2 (Input.GetTouch (i).position.x, Screen.height - Input.GetTouch (i).position.y);
					Vector2 deltagrag = (controllerPositionNext - controllerPositionTemp);
					Data.Azimuth = rotationXtemp + (deltagrag.x * 0.01f * Time.deltaTime);
					Data.Zenith = rotationYtemp + (-deltagrag.y * 0.01f * Time.deltaTime);
					controllerPositionTemp = Vector2.Lerp (controllerPositionTemp, controllerPositionNext, 0.5f);
				}	
			}
		}
	}
	
}
