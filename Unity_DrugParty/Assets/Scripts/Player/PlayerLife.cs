using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour{

	const float width = 0.4455351f;
	const float height = 0.08772884f;

	public GameObject Red;

	public int maxHP = 100;
	public int currentHP = 1;

	SpriteRenderer spriteRenderer; 

    void Start()
    {//0.4455351
    	spriteRenderer = Red.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        spriteRenderer.size = new Vector2(currentHP/maxHP*width,height);
    }

}
