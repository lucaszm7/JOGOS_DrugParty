using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Level3{
	public class CS_Principal : CutsceneController{

		void Start(){
			Go();
		}

		public override void Finish(){
			GameController.audioSource.Play();
			GameObject.FindWithTag("GameController").AddComponent<LevelController>();
			base.Finish();
		}
	}
}