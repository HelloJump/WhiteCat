/// <summary>
/// Teleporter hit enter. using as teleporter with out GUI click
/// </summary>
using UnityEngine;
using System.Collections;

public class TeleporterHitEnter : Teleporter {


	void Start(){
		PlayerManager playerManage = (PlayerManager)GameObject.FindObjectOfType(typeof(PlayerManager));
		if(playerManage)
		playerManage.Teleport(this);	
	}
	
	void Update(){
		if(base.entering){
			Enter();	
		}
	}


}
