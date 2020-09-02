using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public static GameObject items;
	static public int level = 1; 
	static public int score = 0;
	static public int time = 0;

	void Awake(){
		GameController.items = GameObject.Find("ItemsColetaveis");
		GameController.items.SetActive(false);
		Debug.Log(GameObject.Find("ItemsColetaveis"));
		instance = this;
	}

	public static void StartTime(){
		instance.StartCoroutine(instance.TimeCount());
	}

	public static void StopTime(){
		instance.StopAllCoroutines();		
	}

	IEnumerator TimeCount(){
		while(true){
			GameController.time++;
			yield return new WaitForSeconds(1f);
		}
	}

	void OnGUI(){

        string timeString = "";
        int time = GameController.time;

        if(time < 60){
            timeString = "00";
        }else{
            int minutes = time / 60;
            if(minutes < 10) timeString += "0";
            timeString += minutes;
        }
        int seconds = (time % 60);
        timeString += ":";
        if(seconds < 10) timeString += "0";
        timeString += seconds;

        GUI.Label(new Rect(Camera.main.pixelWidth - 100, 10, 50, 20),timeString);

    }

}
