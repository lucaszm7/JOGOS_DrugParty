using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{

    void OnCollisionEnter2D(Collision2D collision){
    	if(collision.gameObject.tag == "Player"){
    		GameObject.FindWithTag("GameController").GetComponent<GameController>().addItem(this);
            gameObject.SetActive(false);

            switch(this.name){
                case "Bebida":
                    Player.bebado = true;
                    break;
                case "Beck":
                    Player.chapado = true;
                    break;
                case "Agua":
                    Player.bebado = false;
                    Player.chapado = false;
                    break;
                case "Doce":

                    break;
            }
            Destroy(gameObject);
    	}
    }
}
