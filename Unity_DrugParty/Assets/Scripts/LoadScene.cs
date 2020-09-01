using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour{
    
    static public string name = "";

    void Start(){
		StartCoroutine(SceneSwitch(LoadScene.name));
    }

    public static void Load(string name){
    	LoadScene.name = name;
        SceneManager.LoadScene(LoadScene.name, LoadSceneMode.Single);
    }

	IEnumerator SceneSwitch(string name){
        AsyncOperation load = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        yield return load;
        SceneManager.UnloadSceneAsync("Loading");
        LoadScene.name = "";
    }

}
