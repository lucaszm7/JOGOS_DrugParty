using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{

	public Sprite bebida;

	[SerializeField]
	int MaxItem = 1;

	int CountItem = 0;
    int tempo = 0;

    bool end = false;
	GameObject[] listItem;
    void Start(){
        listItem = new GameObject[MaxItem];
        StartCoroutine(ContarTempo());
    }

    void OnGUI(){
        GUILayout.BeginArea(new Rect(Camera.main.pixelWidth-30, 10, 30, 20));
        GUILayout.Label(""+tempo);
        GUILayout.EndArea();

    }

    IEnumerator ContarTempo(){
        while(!end){
            tempo += 1;
            yield return new WaitForSeconds(1);
        }
    }

    public void addItem(){
    	float x = -8f,y = 4.74f;
    	x += ++CountItem;

    	GameObject newItem = new GameObject("Item X:"+x+" Y:"+y);
        newItem.transform.Translate(x,y, 0);
        newItem.transform.parent = transform;
        newItem.transform.localScale = new Vector3(2,2,2);
        SpriteRenderer spriteRenderer = newItem.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = bebida;
    }
}
