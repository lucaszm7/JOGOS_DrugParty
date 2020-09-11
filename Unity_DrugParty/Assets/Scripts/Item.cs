using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    Vector3 posicaoInicial;
    void Awake()
    {
        posicaoInicial = transform.position;
    }

    public void Reiniciar()
    {
        transform.position = posicaoInicial;
    }

    void Update()
    {
        if(Player.chapado)
        {
            this.transform.Translate(Vector3.right * 0.5f * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
    	if(collision.gameObject.tag == "Player"){
    		GameObject.FindWithTag("GameController").GetComponent<GameController>().addItem(this);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    
            switch(this.name){
                case "Bebida":
                    PlayerLife.SetHP(-2);
                    Player.bebado = true;
                    break;
                case "Beck":
                    GameObject.FindWithTag("Player").GetComponent<Player>().Impulse();
                    GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(GameObject.FindWithTag("Player").GetComponent<Transform>().position.x, GameObject.FindWithTag("Player").GetComponent<Transform>().position.y + 0.7f, GameObject.FindWithTag("Player").GetComponent<Transform>().position.z);
                    PlayerLife.SetHP(-2);
                    Player.chapado = true;
                    break;
                case "Agua":
                    Debug.Log("PEGOU AGUA!!!");
                    PlayerLife.SetHP(2);
                    Player.bebado = false;
                    Player.chapado = false;
                    break;
                case "Doce":
                    PlayerLife.SetHP(-2);
                    Player.drogado = true;
                break;
            }
            StartCoroutine(SoundAndDestroy());
    	}
    }

    IEnumerator SoundAndDestroy(){
        SoundItem();
        yield return new WaitForSeconds(0.245f);
        gameObject.SetActive(false);
    }

    void SoundItem(){
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("pegar");
        source.Play();
    }
}
