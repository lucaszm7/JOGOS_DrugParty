using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour{

	public static bool isJump = false;

    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "Floor"){
        	PlayerJump.isJump = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
    	if(collision.gameObject.tag == "Floor"){
        	PlayerJump.isJump = false;
        }
    }
}
