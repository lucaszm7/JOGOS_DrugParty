﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{

    void Start(){
        
    }

    void Update(){
        
    }

    void OnCollisionEnter2D(Collision2D collision){
    	if(collision.gameObject.tag == "Player"){
    		GameObject.FindWithTag("GameController").GetComponent<GameController>().addItem();
    		Destroy(gameObject);
    	}
    }
}
