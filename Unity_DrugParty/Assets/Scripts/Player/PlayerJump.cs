using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour{

	public static bool isJump = false;
	public static bool isCollider = false;

    void OnTriggerEnter2D(Collider2D collision){
    	if(collision.gameObject.tag != "item" && collision.gameObject.tag != "Player"){
	        PlayerJump.isCollider = true;
	    }
    }
    void OnTriggerExit2D(Collider2D collision){
    	if(collision.gameObject.tag != "item" && collision.gameObject.tag != "Player"){
    		PlayerJump.isCollider = false;
    	}
    }
}
