using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour{

	[SerializeField]
	Sprite spriteGlitch;
	Sprite spriteNormal;
	bool isGlitch;
	SpriteRenderer spriteRenderer;
	bool actived = false;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteNormal = spriteRenderer.sprite;
	}

	void Update(){
		if(!actived && Player.drogado){
            actived = true;
            StartCoroutine(GlitchAnimation());
        }
        if(actived && !Player.drogado){
            actived = false;
            StopAllCoroutines();
        }
	}

	IEnumerator GlitchAnimation(){
		while(true){
			spriteRenderer.sprite = isGlitch ? spriteGlitch : spriteNormal;
			isGlitch = !isGlitch;
 			yield return new WaitForSeconds(Random.Range(0.1f,0.4f));
		}
	}

}
