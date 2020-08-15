using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Objeto : MonoBehaviour
{
    GameObject Interacao = null;
    bool Colidiu = false;
    public Sprite InteracaoImagem;

    void FixedUpdate()
    {
        if (Input.GetKey("f") && Colidiu)
        {
            if(gameObject.tag == "Finish")
            {
                GameController.Finish();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Interacao == null)
        {
            Colidiu = true;
            Interacao = new GameObject("Interacao: " + this.name);
            Interacao.transform.position = this.transform.position;
            Interacao.transform.parent = this.transform;
            SpriteRenderer spriteRenderer = Interacao.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = InteracaoImagem;
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
        yield return new WaitForSeconds(1);
        Interacao.SetActive(false);
        Colidiu = false;

    }
}
