using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1{
	public class CS_Bar : CutsceneController {

		public override void Finish(){
			GameController.items.SetActive(true);
			GameObject.FindWithTag("Finish").GetComponent<BoxCollider2D>().enabled = true;
			base.Finish();
		} 
	}
}