using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public static AudioSource audioSource;
	public static GameObject items;
	public static int level = 3; 
	public static int score = 0;
	public static int time = 0;

	HashSet<string> Itens = new HashSet<string>();

	public Sprite bebida;
    public Sprite doce;
    public Sprite beck;
    public Sprite agua;


	void Awake(){
		GameController.audioSource = GetComponent<AudioSource>();
		GameController.items = GameObject.Find("ItemsColetaveis");
		GameController.items.SetActive(false);
		instance = this;
	}

	public static void StartTime(){
		instance.StartCoroutine(instance.TimeCount());
	}

	public static void StopTime(){
		instance.StopAllCoroutines();		
	}

	IEnumerator TimeCount(){
		while(true){
			GameController.time++;
			yield return new WaitForSeconds(1f);
		}
	}

	void OnGUI(){

        string timeString = "";
        int time = GameController.time;

        if(time < 60){
            timeString = "00";
        }else{
            int minutes = time / 60;
            if(minutes < 10) timeString += "0";
            timeString += minutes;
        }
        int seconds = (time % 60);
        timeString += ":";
        if(seconds < 10) timeString += "0";
        timeString += seconds;

        GUI.Label(new Rect(Camera.main.pixelWidth - 100, 10, 50, 20),timeString);

    }

     public void addItem(Item itemPass){
        if (Itens.Contains(itemPass.name) == true)
        {
            return;
        }
    	float x = Camera.main.transform.position.x, y = Camera.main.transform.position.y;
        x = x - 1.1f;
        y = y + 1.1f;
        float newItemx = 0.25f;
        x += newItemx * Itens.Count;
        Itens.Add(itemPass.name);

        GameObject newItem = new GameObject("Item X: "+ x +" Y: "+ y);
        newItem.transform.localPosition = new Vector3(x, y, 0);
        newItem.transform.parent = GameObject.Find("PlayerCamera").transform;
        newItem.transform.localScale = new Vector3(1,1,1);
        SpriteRenderer spriteRenderer = newItem.AddComponent<SpriteRenderer>();
        switch (itemPass.name)
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
        spriteRenderer.sortingOrder = 30;
    }

}
