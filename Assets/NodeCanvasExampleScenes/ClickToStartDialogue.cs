using UnityEngine;
using System.Collections;
using NodeCanvas.DialogueTrees;

[RequireComponent(typeof(BoxCollider))]
public class ClickToStartDialogue : MonoBehaviour {

	public DialogueTree dialogueTree;

	void OnMouseDown(){

		if (dialogueTree != null){
			dialogueTree.StartGraph(OnGraphFinished);
			gameObject.SetActive(false);
		}
	}

	void OnGraphFinished(){

		gameObject.SetActive(true);
	}
}
