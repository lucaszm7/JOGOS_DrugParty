﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;

public enum ObjectType{
    Nenhum,
    Saida,
    Escada,
    Cadeira
}


public class Objeto : MonoBehaviour
{
    GameObject Interacao = null;
    bool Colidiu = false;
    [SerializeField]
    ObjectType type;
    public Sprite InteracaoImagem;
    public bool Interagiu;
    bool foi = false;

    void FixedUpdate()
    {
        if (Input.GetKey("f") && Colidiu && !foi){
            foi = true;
            switch(type){
                case ObjectType.Saida:
                    //GameController.Finish();
                break;
                case ObjectType.Escada:
                    //EscadasScrpt.Teste();
                break;  
                case ObjectType.Cadeira:
                   // gameObject.AddComponent(Type.GetType("CS_Bar"));
                    gameObject.AddComponent<CutsceneController>();
                    CutsceneController script = gameObject.GetComponent<CutsceneController>();
                    script.SetName("Bar");
                    script.Go();
                break;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Interacao == null)
        {
            Colidiu = true;
            Interacao = new GameObject("Interacao: " + this.name);
            Interacao.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
            Interacao.transform.parent = this.transform;
            SpriteRenderer spriteRenderer = Interacao.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = InteracaoImagem;
            spriteRenderer.sortingOrder = 30;
            StartCoroutine(DestroiFilho());
        }
        else if (collision.gameObject.tag == "Player")
        {
            Colidiu = true;
            Interacao.SetActive(true);
            StartCoroutine(DestroiFilho());
        }
    }
    IEnumerator DestroiFilho()
    {
        yield return new WaitForSeconds(2);
        Interacao.SetActive(false);
        Colidiu = false;

    }
}
