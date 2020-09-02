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
    void Awake()
    {
        gameObject.AddComponent<CutsceneController>();
        script = gameObject.GetComponent<CutsceneController>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey("f") && Colidiu && !foi){
            foi = true;
            switch(type){
                case ObjectType.Saida:
                    script.SetName("Part2");
                    script.Go();
                    Player.fase2 = true;
                    break;
                case ObjectType.Escada:
                    //EscadasScrpt.Teste();
                    script.SetName("Part3");
                    script.Go();
                    break;  
                case ObjectType.Cadeira:
                    // gameObject.AddComponent(Type.GetType("CS_Bar"));
                    script.SetName("Bar");
                    script.Go();
                    GameObject bebida = (GameObject)Instantiate(Resources.Load("PreFab/Bebida"), this.transform.position, quaternion.identity);
                    bebida.SetActive(true);
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
            Animator animatorRenderer = Interacao.AddComponent<Animator>();
            animatorRenderer.runtimeAnimatorController = InteracaoAnimator;
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
