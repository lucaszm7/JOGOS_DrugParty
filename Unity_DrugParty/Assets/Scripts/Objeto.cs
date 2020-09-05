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
    Bar_Level1
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
    CS_Bar cs;

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

    void OnTrigger2Enter2D(Collider2D collision){
        /*
        if (collision.gameObject.tag == "Player" && Interacao == null)
        {
            if (this.name == "Saida" && LevelController.levelCS != 1)
            {
                return;
            }
            Colidiu = true;
            Interacao = new GameObject("Interacao: " + this.name);
            Interacao.SetActive(false);
            Interacao.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
            Interacao.transform.parent = this.transform;
            SpriteRenderer spriteRenderer = Interacao.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = InteracaoImagem;
            spriteRenderer.sortingOrder = 30;
            Animator animatorRenderer = Interacao.AddComponent<Animator>();
            animatorRenderer.runtimeAnimatorController = InteracaoAnimator;
            
            //StartCoroutine(DesestabilizaFilho());
        }/*
        else if (collision.gameObject.tag == "Player" && !foi)
        {
            Colidiu = true;
            StartCoroutine(DesestabilizaFilho());
        }*/
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            Interacao.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            Debug.Log(Input.GetKey("f"));
            if(Input.GetKey("f")){
                 switch(type){
                    case ObjectType.Saida:
                        LoadScene.Load("Level2");
                        /*
                        if(LevelController.levelCS == 1)
                        {

                            LevelController.levelCS += 1;
                            //==============
                            Debug.Log(LevelController.levelCS);
                            //==============
                            StartCoroutine(DestroiFilho());
                            cs.SetName("Part2");
                            cs.Go();
                        }
                        Player.fase2 = true;*/
                        break;
                    case ObjectType.Escada:
                        if (LevelController.levelCS == 2)
                        {
                            LevelController.levelCS += 1;
                            //==============
                            Debug.Log(LevelController.levelCS);
                            //==============
                            StartCoroutine(DestroiFilho());
                            cs.SetName("Part3");
                            cs.Go();
                        }
                        break;  
                    case ObjectType.Bar_Level1:
                        cs = gameObject.AddComponent<CS_Bar>();
                        cs.SetName("Bar");
                        cs.Go();
                        //GameObject.Destroy();
                        GetComponent<BoxCollider2D>().enabled = false;
                    break;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
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
