using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3{
	public class LevelController : MonoBehaviour{

		public static bool PlayerMiny = false;

		GameObject Player;
		Player ScriptPlayer;
		Vector3 _currentScale;
		float _minScale = 0.4f;
		float _maxScale = 1f;


		void Start(){
			Player = GameObject.FindWithTag("Player");
			_currentScale = Player.transform.localScale;
			ScriptPlayer = Player.GetComponent<Player>();
			ScriptPlayer.isPaused = true;
			ScriptPlayer.velocity = 2f;
			ScriptPlayer.salto = 1.6f;
			LevelController.PlayerMiny = true;
	        StartCoroutine(TransformPlayer());
	    }

	    IEnumerator TransformPlayer(){
	    	int i = 0;
	    	Vector3 scale = Camera.main.transform.localScale;
	    	float size = 1.3f;
	    	while(_currentScale.x > _minScale){
	    		// Tamanho do personagem
	    		_currentScale -= new Vector3(0.04f,0.04f,0);
	    		Player.transform.localScale = _currentScale;
	    		i++;
	    		Player.transform.localPosition -= new Vector3(0f,(0.1732f/15f),0f);

	    		// Tamanho da Camera
	    		size -= Mathf.Abs((1.3f - 0.7f) / 15f);
		    	Camera.main.orthographicSize = size;	
	    		
	    		// Tamanho da Escala da Camera
	    		float numberScale = Mathf.Abs((1f - 0.5f) / 15f);
				scale -= new Vector3(numberScale,numberScale,0);
			    Camera.main.transform.localScale = scale;

			    // Posição de Camera
			    Camera.main.transform.localPosition -= new Vector3(0f,Mathf.Abs(-0.37f / 15f),0f);
		    	yield return new WaitForSeconds(0.1f);
		    }
			ScriptPlayer.isPaused = false;
	    }

	    void OnTriggerEnter2D(Collider2D collision){
	        if(collision.gameObject.tag == "Player"){
	        	GetComponent<BoxCollider2D>().enabled = false;
		        ScriptPlayer.isPaused = true;
				ScriptPlayer.velocity = 3f;
				ScriptPlayer.salto = 1.3f;
	        	StartCoroutine(TransformNormalPlayer());				
	        }
	    }

	    IEnumerator TransformNormalPlayer(){
	    	int i = 0;
	    	Vector3 scale = Camera.main.transform.localScale;
	    	float size = 0.7f;
	    	while(_currentScale.x <= _maxScale){
	    		// Tamanho do personagem
	    		_currentScale += new Vector3(0.04f,0.04f,0);
	    		Player.transform.localScale = _currentScale;
	    		i++;
	    		Player.transform.localPosition -= new Vector3(0f,(0.1732f/16f),0f);

	    		// Tamanho da Camera
	    		size += Mathf.Abs((1.3f - 0.7f) / 16f);
		    	Camera.main.orthographicSize = size;	
	    		
	    		// Tamanho da Escala da Camera
	    		float numberScale = Mathf.Abs((1f - 0.5f) / 16f);
				scale += new Vector3(numberScale,numberScale,0);
			    Camera.main.transform.localScale = scale;

			    // Posição de Camera
			    Camera.main.transform.localPosition += new Vector3(0f,Mathf.Abs(0.37f / 16f),0f);
		    	yield return new WaitForSeconds(0.1f);
		    }
		    Camera.main.transform.localScale = new Vector3(1f,1f,1f);
	    	Camera.main.orthographicSize = 1.3f;	
		    LevelController.PlayerMiny = false;
			ScriptPlayer.isPaused = false;
	    }
	}
}