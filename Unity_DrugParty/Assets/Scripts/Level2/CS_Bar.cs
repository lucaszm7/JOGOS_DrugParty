using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2{
	public class CS_Bar : CutsceneController {

		public override void Finish(){
	        PlayerLife.SetHP(100);
			base.Finish();
		} 
	}
}