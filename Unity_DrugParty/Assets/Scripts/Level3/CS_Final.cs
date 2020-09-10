using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// CS => CutScene
public class CS_Final : CutsceneController{


	internal override void Go(){
		SceneManager.LoadScene("MusicFinal",LoadSceneMode.Additive);
		base.Go();
	}

	public override void Finish(){
		StartCoroutine(SceneSwitch());
		//LoadScene.Load("Main");
	}

	IEnumerator SceneSwitch(){
        AsyncOperation load = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        yield return load;
        SceneManager.UnloadSceneAsync("Level3");
    }

	internal override void Step(){
		base.Step();
		if(_currentScene.Id == 74){
			Debug.Log("Mudou");
			GameObject.Find("MusicFinal").GetComponent<AudioSource>().Play();
			/*
			AudioSource audio = GameObject.FindWithTag("Cutscene").GetComponent<AudioSource>();
			audio.clip = Resources.Load<AudioClip>("proerd");
			audio.Play();*/
		}
	}
}
