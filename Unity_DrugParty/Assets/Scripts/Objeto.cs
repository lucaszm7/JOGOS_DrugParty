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
    Cadeira
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
    void Awake()
    {
        gameObject.AddComponent<CutsceneController>();
        cs = gameObject.AddComponent<CS_Bar>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey("f") && Colidiu && !foi){
            foi = true;
            switch(type){
                case ObjectType.Saida:
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
                    Player.fase2 = true;
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
                case ObjectType.Cadeira:
                    LevelController.levelCS += 1;
                    //==============
                    Debug.Log(LevelController.levelCS);
                    //=============
                    StartCoroutine(DestroiFilho());
                    cs.SetName("Bar");
                    cs.Go();
                break;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Interacao == null)
        {
            if (this.name == "Saida" && LevelController.levelCS != 1)
            {
                return;
            }
            Colidiu = true;
            Interacao = new GameObject("Interacao: " + this.name);
            Interacao.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
            Interacao.transform.parent = this.transform;
            SpriteRenderer spriteRenderer = Interacao.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = InteracaoImagem;
            spriteRenderer.sortingOrder = 30;
            Animator animatorRenderer = Interacao.AddComponent<Animator>();
            animatorRenderer.runtimeAnimatorController = InteracaoAnimator;
            
            StartCoroutine(DesestabilizaFilho());
        }
        else if (collision.gameObject.tag == "Player" && !foi)
        {
            Colidiu = true;
            StartCoroutine(DesestabilizaFilho());
        }
    }
    IEnumerator DestroiFilho()
    {
        yield return new WaitForSeconds(2);
        Interacao.SetActive(false);
        Colidiu = false;
    }
    IEnumerator DesestabilizaFilho()
    {
        yield return new WaitForSeconds(2);
        Colidiu = false;
    }
}
