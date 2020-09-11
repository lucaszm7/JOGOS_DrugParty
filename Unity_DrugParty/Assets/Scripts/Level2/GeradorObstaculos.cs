using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorObstaculos : MonoBehaviour
{
    [SerializeField]
    private GameObject manualInstrucoes;
    [SerializeField]
    private float timeGenerate;
    [SerializeField]
    private float range;
    [SerializeField]
    private int nObstaculos;
    [SerializeField]
    private GameObject agua;

    public static int pontos = 0;

    private Vector3 altura = new Vector3();
    private float cronometer;

    public static int ObsDestroy = 0;

    void Awake()
    {
        this.nObstaculos = 0;
    }

    void Reiniciar()
    {
        this.nObstaculos = 0;
    }

    void Update()
    {
        if(!Player.chapado)
        {
            return;
        }
        //Quando? Tempo(3s)
        this.cronometer -= Time.deltaTime;
        if (this.cronometer <= 0 && this.nObstaculos < 10)
        {
            //Onde? Na posição do gerador
            //Como criar nossos objetos
            this.nObstaculos++;
            altura = this.transform.position;
            altura.y = altura.y + Random.Range(-(range), range);
            GameObject.Instantiate(manualInstrucoes, altura, Quaternion.identity);
            if (nObstaculos == 10)
            {
                agua.SetActive(true);
                altura.x += 17.5f;
                agua.transform.position = altura;
            }
            this.cronometer = this.timeGenerate;
        }
    }
}

