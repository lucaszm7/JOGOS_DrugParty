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

    	//0.2392
    	//0.0178
    	spriteRenderer = Red.GetComponent<SpriteRenderer>();
    	//Debug.Log(spriteRenderer.transform.position);
    }

    // Update is called once per frame
    void Update(){
    	float total = Mathf.Clamp01((float)currentHP/(float)maxHP);
    	spriteRenderer.transform.localPosition = new Vector3(0.0178f+(total*(0.2392f-0.0178f)),spriteRenderer.transform.localPosition.y,spriteRenderer.transform.localPosition.z);
        //spriteRenderer.transform.position += new Vector3(0.0178f+(currentHP/maxHP*0.2392f),0,0);
        //Debug.Log(currentHP/maxHP*0.4455351f);
    	//Debug.Log(total);
        
        spriteRenderer.size = new Vector2(total*width,height);
    }

}
