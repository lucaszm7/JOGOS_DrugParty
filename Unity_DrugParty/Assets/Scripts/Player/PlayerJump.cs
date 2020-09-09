using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour{

	public static bool isJump = false;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Floor"){
	    	Debug.Log("Entrou");
        	PlayerJump.isJump = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
	    	Debug.Log("Saiu");
    	if(collision.gameObject.tag == "Floor"){
        	PlayerJump.isJump = false;
        }
    }
}
