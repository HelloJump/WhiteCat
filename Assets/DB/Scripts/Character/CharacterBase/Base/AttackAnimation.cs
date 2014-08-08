using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackAnimation{
	public AnimationClip Motion;
	public string AttackName;//attack animation
	public float AttackTime;// time to sync with attack animation
	public AnimationBlendMode BlendMode;
}
