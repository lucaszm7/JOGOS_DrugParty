using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrousel : MonoBehaviour
{
    Vector3 posicaoInicial;
    void Awake()
    {
        posicaoInicial = this.transform.position;
    }

    public void Reiniciar()
    {
        this.transform.position = posicaoInicial;
    }

    void Update()
    {
        if (Player.chapado)
        {
            if(this.tag == "Fundo")
            {
                this.transform.Translate(Vector3.right * 0.1f * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.right * 0.3f * Time.deltaTime);
            }
        }
    }
}
