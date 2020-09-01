using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{

    void OnCollisionEnter2D(Collision2D collision){
    	if(collision.gameObject.tag == "Player"){
    		GameObject.FindWithTag("GameController").GetComponent<LevelController>().addItem(this);
            gameObject.SetActive(false);
            if(this.name == "Bebida")
            {
                Player.bebado = true;
            }
            else if(this.name == "Agua")
            {
                Player.bebado = false;
            }
            //Destroy(gameObject);
    	}
    }
}
