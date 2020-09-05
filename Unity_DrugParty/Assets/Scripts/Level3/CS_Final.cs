using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CS => CutScene
public class CS_Final : CutsceneController{

	public override void Finish(){
		LoadScene.Load("Main");
	}
}
