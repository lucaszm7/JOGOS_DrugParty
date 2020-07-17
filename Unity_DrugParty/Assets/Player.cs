using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D physics;
    void Awake()
    {
        this.physics = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.physics.AddForce(Vector3.right * 5);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.physics.AddForce(Vector3.left * 5);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.physics.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        }
    }
}
