using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	static public int level = 1; 
	static public int score = 0;
	static public int time = 0;

	void Awake(){
		instance = this;
	}

	public static void StartTime(){
		instance.StartCoroutine(instance.TimeCount());
	}

	IEnumerator TimeCount(){
		while(true){
			GameController.time++;
			yield return new WaitForSeconds(0.05f);
		}
	}

}
