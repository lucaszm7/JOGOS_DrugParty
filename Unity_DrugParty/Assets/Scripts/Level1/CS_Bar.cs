using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Bar : CutsceneController {

	public override void Finish(){
		Debug.Log("Finalizou");
		GameController.items.SetActive(true);
		base.Finish();
	} 
}
