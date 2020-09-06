using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3{
	public class LevelController : MonoBehaviour{

		GameObject Player;
		Player ScriptPlayer;
		Vector3 _currentScale;
		float _minScale = 0.4f;


		void Start(){
			Player = GameObject.FindWithTag("Player");
			_currentScale = Player.transform.localScale;
			ScriptPlayer = Player.GetComponent<Player>();
			ScriptPlayer.isPaused = true;
	        StartCoroutine(TransformPlayer());
	        StartCoroutine(TransformCamera());
	    }

	    // -1.47 x
	    // -0.38 y

	    IEnumerator TransformPlayer(){
	    	while(_currentScale.x > _minScale){
	    		_currentScale -= new Vector3(0.06f,0.06f,0);
	    		Player.transform.localScale = _currentScale;
		    	yield return new WaitForSeconds(0.2f);
		    }
	    }

	    IEnumerator TransformCamera(){
	    	float size = 1.3f;
	    	Vector3 scale = Camera.main.transform.localScale;
	    	while(Camera.main.transform.localScale.x >= 0.6f){
	    		if(size > 0.7f){
		    		size -= 0.05f;
		    		Camera.main.orthographicSize = size;
		    	}
	    		scale -= new Vector3(0.05f,0.05f,0);
			    Camera.main.transform.localScale = scale;
		    	yield return new WaitForSeconds(0.6f);
	    	}
			ScriptPlayer.isPaused = false;
	    }

	    void Update(){
	        
	    }
	}
}