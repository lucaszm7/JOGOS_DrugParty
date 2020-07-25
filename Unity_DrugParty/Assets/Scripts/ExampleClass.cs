using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    void Start()
    {
        print("Iniciando " + Time.time);
        StartCoroutine(EsperarImprimir(2.0F));
        Debug.Log("Antes da Co-Routine acabar " + Time.fixedTime);
    }
    IEnumerator EsperarImprimir(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("EsperarImprimir " + Time.time);
    }
}