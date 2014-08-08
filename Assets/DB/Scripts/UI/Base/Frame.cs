using UnityEngine;
using System.Collections;

public class Frame {
	[HideInInspector]
	public Vector2 Position,tempPosition;
	[HideInInspector]
	public bool Show,OnDrag;
	[HideInInspector]
	public PlayerCharacterUI PlayerUI;

	
	public virtual void Inz(Vector2 position,PlayerCharacterUI ui){
		Position = position;
		PlayerUI = ui;
	}

	public void OnDraging(){
		if(!OnDrag){
			tempPosition = Position - new Vector2(Input.mousePosition.x,-Input.mousePosition.y);
			OnDrag = true;
		}else{
			OnDrag = false;	
		}
	}
	
	public void Update () {
		if(OnDrag){
			Position = tempPosition + new Vector2(Input.mousePosition.x,-Input.mousePosition.y);	
		}
	}
}
