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
                    PlayerLife.SetHP(-2);
                    Player.bebado = true;
                    break;
                case "Beck":
                    PlayerLife.SetHP(-2);
                    Player.chapado = true;
                    break;
                case "Agua":
                    PlayerLife.SetHP(2);
                    Player.bebado = false;
                    Player.chapado = false;
                    break;
                case "Doce":
                    PlayerLife.SetHP(-2);
                    Player.drogado = true;
                break;
            }
            Destroy(gameObject);
    	}
    }
}
