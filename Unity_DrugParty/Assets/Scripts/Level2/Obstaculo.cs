using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField]
    private float velocity = 0.7f;
    private Vector3 posicaoPlayer;
    private bool pontuei = false;
    void OnTriggerEnter2D(Collider2D ObjetoColidido)
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        this.posicaoPlayer = GameObject.FindObjectOfType<Player>().transform.position;
    }
    void Update()
    {
        this.transform.Translate(Vector3.right * this.velocity * Time.deltaTime);
        if (!this.pontuei && this.transform.position.x < this.posicaoPlayer.x)
        {
            GeradorObstaculos.pontos++;
            this.pontuei = true;
        }
    }

    public void Destuir()
    {
        Destroy(this.gameObject);
    }
}
