using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInteraction : MonoBehaviour
{
    Transform []Objetos;
    Player Player;
    void Awake()
    {
        
    }
    void Start()
    {
        Objetos = GetComponentsInChildren<Transform>();
        Player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        foreach (Transform objeto in Objetos)
        {
            if (Player.transform.position == objeto.position)
            {
                Debug.Log("Colisão");
            }
        }
    }
}
