using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit{

    public float velocity;
    Rigidbody2D physics;

    
    void Awake(){
        this.physics = GetComponent<Rigidbody2D>();
    }
	
	void LateUpdate(){
    	float positionY = transform.position.y;
    	if(positionY < 0) positionY = 0;
    	Camera.main.transform.position =  new Vector3(transform.position.x,positionY,-10f);
    }

    void FixedUpdate(){
		float move = Input.GetAxis("Horizontal");

		physics.velocity = new Vector2( move * velocity, physics.velocity.y);
    }

    void Update2(){
        if (Input.GetKey(KeyCode.D))
        {
            this.physics.AddForce(Vector3.right * velocity);
        }
        if (Input.GetKey(KeyCode.A))
        {
        //    this.physics.AddForce(Vector3.left * Velocity);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.physics.AddForce(Vector3.up * velocity, ForceMode2D.Impulse);
        }
    }
}
