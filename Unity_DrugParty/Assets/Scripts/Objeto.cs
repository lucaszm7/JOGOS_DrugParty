using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;
using Unity.Mathematics;

public enum ObjectType{
    Nenhum,
    Saida,
    Escada,
    Bar_Level1,
    Bar_Level2,
    Balada
}


public class Objeto : MonoBehaviour
{
    [SerializeField]
    RuntimeAnimatorController InteracaoAnimator;

    GameObject Interacao = null;
    bool Colidiu = false;
    [SerializeField]
    ObjectType type;
    public Sprite InteracaoImagem;
    public bool Interagiu;
    bool foi = false;
    CutsceneController script;

    void Awake(){
        Interacao = new GameObject("Interacao: " + this.name);
        Interacao.SetActive(false);
        Interacao.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
        Interacao.transform.parent = this.transform;
        SpriteRenderer spriteRenderer = Interacao.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = InteracaoImagem;
        spriteRenderer.sortingOrder = 30;
        Animator animatorRenderer = Interacao.AddComponent<Animator>();
        animatorRenderer.runtimeAnimatorController = InteracaoAnimator;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            foi = false;
            Interacao.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "Player" && !foi){
            if(Input.GetKey("f")){
                foi = true;
                 switch(type){
                    case ObjectType.Saida:  
                        LoadScene.Load("Level2");
                        GameController.level++;
                    break;
                    case ObjectType.Balada:
                        CS_Final cs_final = gameObject.AddComponent<CS_Final>();
                        cs_final.SetName("Final");
                        cs_final.Go();
                        GameController.level = 1;
                    break;
                    case ObjectType.Escada:
                    
                    break;  
                    case ObjectType.Bar_Level1:
                        Level1.CS_Bar bar1 = gameObject.AddComponent<Level1.CS_Bar>();
                        bar1.SetName("Bar_Level1");
                        bar1.Go();
                        //GameObject.Destroy();
                        GetComponent<BoxCollider2D>().enabled = false;
                    break;
                    case ObjectType.Bar_Level2:
                        LoadScene.Load("Level3");
                        GameController.level++;
                        /*Level2.CS_Bar bar2 = gameObject.AddComponent<Level2.CS_Bar>();
                        bar2.SetName("Bar_Level2");
                        bar2.Go();
                        //GameObject.Destroy();
                        GetComponent<BoxCollider2D>().enabled = false;*/
                    break;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            foi = false;
            StartCoroutine(DestroiFilho());
        }
    }

    IEnumerator DestroiFilho(){
        yield return new WaitForSeconds(0.25f);
        Interacao.SetActive(false);
        Colidiu = false;
    }
    IEnumerator DesestabilizaFilho()
    {
        yield return new WaitForSeconds(2);
        Colidiu = false;
    }
}
