using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CS => CutScene
public class CS_Principal : CutsceneController{

	void Start(){
		Debug.Log("foi");
		Go();
	}

	public override void Finish(){
		GameController.audioSource.Play();
		base.Finish();
	}
}
