using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDead : MonoBehaviour{

	public Vector3 spawn;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision){
    	Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player"){
        	//GameObject.FindWithTag("Player").GetComponent<Transform>().transform.localPositon = spawn;
        	collision.gameObject.transform.localPosition = spawn;
        	PlayerLife.SetHP(-1);
        }
    }
}
