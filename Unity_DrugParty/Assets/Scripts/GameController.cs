using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{

	public Sprite bebida;
    public Sprite doce;
    public Sprite beck;
    public Sprite agua;

    [SerializeField]
	int MaxItem = 10;

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
        GUILayout.Label("=== TEMPO ==="+tempo);
        GUILayout.EndArea();

    }

    IEnumerator ContarTempo(){
        while(!end){
            tempo += 1;
            yield return new WaitForSeconds(1);
        }
    }

    public void addItem(string name){
    	float x = Camera.main.transform.position.x, y = Camera.main.transform.position.y;
        x = x - 3.1f;
        y = y - 1.2f;
        float newItemx = 0.2f;
        x += newItemx * CountItem;
        ++CountItem;

        GameObject newItem = new GameObject("Item X:"+x+" Y:"+y);
        newItem.transform.localPosition = new Vector3(x,y, 0);
        newItem.transform.parent = Camera.main.transform;
       // newItem.transform.localScale = new Vector3(2,2,2);
        SpriteRenderer spriteRenderer = newItem.AddComponent<SpriteRenderer>();
        switch (name)
        {
            case "Beck":
                spriteRenderer.sprite = beck;
                break;
            case "Agua":
                spriteRenderer.sprite = agua;
                break;
            case "Doce":
                spriteRenderer.sprite = doce;
                break;
            case "Bebida":
                spriteRenderer.sprite = bebida;
                break;
        }
        //spriteRenderer.sprite = bebida;
        spriteRenderer.sortingOrder = 30;
        Debug.Log(newItem.transform.localPosition);
    }

    public static void Finish(){
        Debug.Log("Acabou");
    }
}
