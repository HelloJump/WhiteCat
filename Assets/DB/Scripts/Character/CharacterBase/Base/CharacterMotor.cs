using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class CharacterMotor : MonoBehaviour
{
	public float GravityMult = 1;
	public float Slip = 10;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 targetDirection = Vector3.zero;
	public CharacterController controller;
	
	void Start ()
	{
		controller = GetComponent<CharacterController> ();
	}
	
	void Update ()
	{
		
		moveDirection = Vector3.Lerp(moveDirection,targetDirection,Time.deltaTime * Slip);
		moveDirection.y -= 90 * GravityMult * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);

	}

	public void Jump (float jumpSpeed)
	{
		moveDirection.y = jumpSpeed;
	}

	public void Move (Vector3 dir)
	{
		if (controller.isGrounded) {
			targetDirection = dir;
		}
	}
	public void Stop(){
		moveDirection = Vector3.zero;
		targetDirection = Vector3.zero;
	}
}

