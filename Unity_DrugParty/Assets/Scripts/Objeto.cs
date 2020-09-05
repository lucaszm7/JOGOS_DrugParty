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
    Bar_Level2
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
                        if(GameController.level == 3){
                            CS_Final cs_final = gameObject.AddComponent<CS_Final>();
                            cs_final.SetName("Final");
                            cs_final.Go();
                        }else{
                            GameController.level++;
                            LoadScene.Load("Level"+GameController.level);
                        }
                    break;
                    case ObjectType.Escada:
                    /*
                        if (LevelController.levelCS == 2)
                        {
                            LevelController.levelCS += 1;
                            //==============
                            Debug.Log(LevelController.levelCS);
                            //==============
                            StartCoroutine(DestroiFilho());
                            cs.SetName("Part3");
                            cs.Go();
                        }*/
                        break;  
                    case ObjectType.Bar_Level1:
                        CS_Bar cs_bar = gameObject.AddComponent<CS_Bar>();
                        cs_bar.SetName("Bar");
                        cs_bar.Go();
                        //GameObject.Destroy();
                        GetComponent<BoxCollider2D>().enabled = false;
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
