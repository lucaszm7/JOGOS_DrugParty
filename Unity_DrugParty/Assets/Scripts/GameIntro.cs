using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntro : MonoBehaviour{

	public void StartGame(){
		//SceneManager.LoadScene("Cutscene", LoadSceneMode.Single);
		LoadScene.Load("Cutscene");
		//Debug.Log("Iniciar o jogo");
 	}

	public void QuitGame(){
        Application.Quit();
		Debug.Log("Sair do jogo");
	}

}
