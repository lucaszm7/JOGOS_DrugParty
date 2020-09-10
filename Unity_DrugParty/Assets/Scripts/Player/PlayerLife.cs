using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour{

	public static PlayerLife instance;

	const float width = 0.4455351f;
	const float height = 0.08772884f;

	public GameObject Red;

	static public int maxHP = 10;
	static public int currentHP = 10;

	SpriteRenderer spriteRenderer; 

	void Awake(){
		instance = this;
	}

    void Start(){
    	spriteRenderer = Red.GetComponent<SpriteRenderer>();
    }

    void Update(){
    	float total = Mathf.Clamp01((float)PlayerLife.currentHP/(float)PlayerLife.maxHP);
    	spriteRenderer.transform.localPosition = new Vector3(0.0178f+(total*(0.2392f-0.0178f)),spriteRenderer.transform.localPosition.y,spriteRenderer.transform.localPosition.z);        
        spriteRenderer.size = new Vector2(total*width,height);
    }

    public static void SetHP(int HP){
    	if(HP > maxHP) HP = maxHP;
    	Debug.Log("SET" + HP);
    	currentHP += HP;
    	if(currentHP < 0) currentHP = 0;
    	if(currentHP == 0){
    		Debug.Log("MORREU");
    		GameObject.FindWithTag("Player").GetComponent<Player>().PlayerDead();
    	}
    }

}
